using System;
using System.Collections.Generic;
using System.Text;

namespace GbTry.Machine
{
    public class PPU
    {
        private GbCPU gbCPU;
        private int PPU_clk = 0;
        private ushort LCDC = 0xFF40; 
        private ushort LCDSTAT = 0xFF41;
        private ushort SCY = 0xFF42;        // scroll y
        private ushort SCX = 0xFF43;        // scroll x
        private ushort LY = 0xFF44;
        private ushort LYC = 0xFF45;
        private ushort INTF = 0xFF0F;
        private ushort WINY = 0xFF4a;
        private ushort WINX = 0xFF4b;
        private ushort OBJPAL0 = 0xFF48; 
        private ushort OBJPAL1 = 0xFF49;
        // backgroud paletter
        private ushort BGRDPAL = 0xFF47;
        private byte [] BGprio  = new byte[160];
        //public byte [,] Pix= new byte[2,69120]; // screen
        public byte Buffer = 0;
        private bool Blink = false;
        private UInt32[] data;

        public void init(ref UInt32[] g_data)
        {
            Buffer = 0;
            Blink = false;
            PPU_clk = 0;
            Array.Clear(BGprio,0,160);
            //Array.Clear(Pix, 0, 2*69120);
            data = g_data;
        }

        public bool blink()
        {
            if (Blink)
            {
                Blink = false;
               /* for (uint i = 0, j = 0; j < 160 * 144; i += 3, j++)
                {
                    var index = Buffer;
                    data[j] = (uint)(Pix[index, i] << 16 | Pix[index, i + 1] << 8 | Pix[index, i + 2]);
                }*/
                return true;
            }
            return false;
        }

        private byte PPU_mode = 0; // 0 - hblank, 1 - vblank, 2 - scanline oam, 3 - scanline vram
        public void GetGbCPU(GbCPU gbCPU)
        {
           this.gbCPU = gbCPU;
        }
        private void blit() {
           Buffer ^= 1;
           Blink = true;
        }

        private byte GetValue(ushort addrsss)
        {
            return gbCPU.GetValueFromMemory(addrsss);
        }

        private void SetValue(ushort addrsss,byte value)
        {
            gbCPU.SetValueIntoMemory(addrsss, value);
        }

        private bool LYIsEqualLYC()
        {
            return GetValue(LY) == GetValue(LYC);
        }
        private void Check_Interrupt_lyc()
        {
            if (LYIsEqualLYC())
            {
                var Lcdstat = GetValue(LCDSTAT);
                SetValue(LCDSTAT, (byte)(Lcdstat | 0x04));
                byte lyc_inte = (byte)(Lcdstat &0x40);
                if (lyc_inte != 0)
                    SetINTF(0x02);
            }
            else
            {
                var Lcdstat = GetValue(LCDSTAT);
                SetValue(LCDSTAT, (byte)(Lcdstat & 0xFB));
            }
        }
        public void Render()
        {
            PPU_clk += gbCPU.Cycle;
            if (PPU_clk >= 456)
            {
                PPU_clk -= 456;
                var ly = GetValue(LY);
                SetValue(LY, (byte)((ly + 1) % 154));
                Check_Interrupt_lyc();
                if (ly == 144) { 
                    PPU_Change_Mode(1); 
                } // vblank
            }
            if (GetValue(LY) < 144) // normal line
            {
                if (PPU_clk <= 80) //scan oam
                {
                    if (PPU_mode != 2) PPU_Change_Mode(2);
                }
                else if (PPU_clk <= (80 + 172)) //scan vram
                { // 252 cycles
                    if (PPU_mode != 3) { PPU_Change_Mode(3); }
                }
                else
                { // remaining 204 hblank
                    if (PPU_mode != 0) { PPU_Change_Mode(0); }
                }
            }
        }

        //mode = 0, 00, display ram can be accessed. cpu ok
        //       1, 01 vblank, cpu access ok
        //       2, 10 during oam-ram, cpu not ok
        //       3, 11,during transferring data to lcd, cpu not ok
        void PPU_Change_Mode(byte new_mode)
        {
            PPU_mode = new_mode;
            switch (PPU_mode)
            {
                case 0:
                    GB_GPU_Model_0(); 
                    PPU_renderscan();
                    break;
                case 1:
                    GB_GPU_Model_1();
                    blit();
                    SetINTF(0x01);break;
                case 2:
                    GB_GPU_Model_2();
                    break;
                case 3:
                    GB_GPU_Model_3();
                    break;
            }
        }

        private void GB_GPU_Model_0() // H-blank
        {
            var Lcdstat = GetValue(LCDSTAT);
            if ((Lcdstat & 3) != 0)
            {
                SetValue(LCDSTAT, (byte)(Lcdstat & 0xFC));// 設定狀態
                if ((GetValue(LCDSTAT) & 8) != 0)
                    SetINTF(0x02); // 設定中斷發生flag
            }
        }
        private void GB_GPU_Model_1() //V-blank
        {
            if ((GetValue(LCDSTAT) & 3) != 1)
            {
                SetValue(LCDSTAT, (byte)((GetValue(LCDSTAT) & 0xFC) | 1));
                if ((GetValue(LCDSTAT) & 0x10) != 0)
                    SetINTF(0x02); // 設定中斷發生flag
                SetINTF(0x01); 
            }
        }
        private void GB_GPU_Model_2() // OAM in use
        {
            if ((GetValue(LCDSTAT) & 3) != 2)
            {
                SetValue(LCDSTAT, (byte)((GetValue(LCDSTAT) & 0xFC) | 2));
                if ((GetValue(LCDSTAT) & 0x20) != 0)
                    SetINTF(0x02); // 設定中斷發生flag
            }
        }

        private void GB_GPU_Model_3() //OAM+VRAM Busy , 沒有中斷功能設定對應的模式
        {
            if ((GetValue(LCDSTAT) & 3) != 3)
               SetValue(LCDSTAT, (byte)(GetValue(LCDSTAT) | 0x03));// 設定狀態
        }
        private void  SetINTF(byte value)
        {
            var intf = GetValue(INTF);
            intf |= value;
            SetValue(INTF, intf);
        }

        void PPU_renderscan()
        {
           PPU_draw_bg();
           PPU_draw_sprites();
        }
        void PPU_draw_bg()
        {
            var Lcdc = GetValue(LCDC);
            byte gpu_win_on = (byte)((Lcdc >> 5) & 0x1);//off,on
            byte lcd_on = (byte)((Lcdc >> 7) & 0x1);//off,on

            if (lcd_on == 0x01)
            {
                byte bgy = (byte)(GetValue(LY) + GetValue(SCY));
                ushort bgtiley = (ushort)((bgy >> 3) & 31);
                int winy = (gpu_win_on == 0x01) ? GetValue(LY) - GetValue(WINY) : -1;
                ushort wintiley = (ushort)((winy >> 3) & 31);
                for (byte x = 0; x < 160; x++)
                {
                    uint bgx = (uint)((int)GetValue(SCX) + (int)x);
                    int winx = -((int)GetValue(WINX) - 7) + x;
                    ushort tilemapbase = 0;
                    ushort tilex = 0, tiley = 0, pixelx = 0, pixely = 0;
                    byte gpu_bgmap = (byte)((Lcdc >> 3) & 0x1);//9800-9bff, 9c00-9fff
                    byte gpu_tilemap = (byte)((Lcdc >> 4) & 0x1);//8800-97ff, 8000-8fff
                    byte gpu_drawbg = (byte)((Lcdc >> 0) & 0x1);//off,on
                    ushort tilebase = (ushort)((gpu_tilemap == 0x01) ? 0x8000 : 0x8800);
                    if (winx >= 0 && winy >= 0)
                    { // draw window
                        byte gpu_win_map = (byte)((Lcdc >> 6) & 0x1);//9800-9bff, 9c00-9fff
                        tilemapbase = (ushort)((gpu_win_map == 0x01) ? 0x9c00 : 0x9800);
                        tiley = wintiley; tilex = (ushort)(winx >> 3); pixely = (ushort)(winy & 0x7); pixelx = (byte)(winx & 0x7);
                    }
                    else if (gpu_drawbg == 0x01)
                    { // draw bg
                        tilemapbase = (ushort)((gpu_bgmap== 0x01) ? 0x9c00 : 0x9800);
                        tiley = bgtiley; tilex = (ushort)((bgx >> 3) & 31); pixely = (ushort)(bgy&0x7); pixelx = (byte)(bgx & 0x7);
                    }

                    ushort value = (ushort)(tilemapbase + tiley * 32 + tilex);
                    byte _tilenr = gbCPU.GetValueFromMemory(value);
                    ushort tilenr, tileaddress;
                    if (tilebase == 0x8800)
                    {
                        sbyte nr_s = (sbyte)_tilenr; 
                        short nr_s16 = (short)(nr_s + 128); 
                        tilenr = (ushort)nr_s16;
                    }
                    else { tilenr = (ushort)_tilenr; }
                    tileaddress = (ushort)(tilenr * 16 + tilebase);

                    ushort a0 = (ushort)(tileaddress + pixely * 2);
                    byte data0 = gbCPU.GetValueFromMemory(a0);
                    byte data1 = gbCPU.GetValueFromMemory((ushort)(a0+1));
                    byte color0_idx = (byte)((data0 >> (7 - pixelx)) & 0x1);
                    byte color1_idx = (byte)((data1 >> (7 - pixelx)) & 0x1);
                    byte color_idx = (byte)(color0_idx  + color1_idx* 2);
                    byte r = 0, g = 0, b = 0;
                    var cor_reg = GetValue(BGRDPAL);
                    byte color = (byte)((cor_reg >> (color_idx * 2)) & 0x3);
                    BGprio[x] = color_idx;
                    if (color == 0) { r = 255; g = 255; b = 255; }
                    if (color == 1) { r = 192; g = 192; b = 192; }
                    if (color == 2) { r = 96; g = 96; b = 96; }
                    if (color == 3) { r = 0; g = 0; b = 0; }

                    uint screen_off = (uint)(GetValue(LY)*160  + x);
                    if (screen_off < 144 * 160 )
                    {
                        data[screen_off] = (uint)(r << 16 | g << 8 | b);
                    }
                }
            }
            else { } // lcd off
        }

        void PPU_draw_sprites()
        {
            var Lcdc = GetValue(LCDC);
            byte gpu_sprite_on = (byte)((Lcdc >> 1) & 0x1);
            byte gpu_sprite_size = (byte)(((Lcdc >> 2) & 0x1) == 0x01 ? 16 : 8);//8x8, 8x16
            if (gpu_sprite_on == 0x01)
            {
                for (byte idx = 0; idx < 40; idx++)
                {
                    byte i = (byte)(39 - idx);
                    ushort spriteaddr = (ushort)(0xfe00 + i * 4);
                    int spritey = gbCPU.GetValueFromMemory((ushort)(spriteaddr + 0)) - 16;
                    int spritex = gbCPU.GetValueFromMemory((ushort)(spriteaddr + 1)) - 8;
                    ushort tilenum = gbCPU.GetValueFromMemory((ushort)(spriteaddr + 2));
                    if (gpu_sprite_size == 8) tilenum &= 0xff; else tilenum &= 0xfe;
                    byte flags = gbCPU.GetValueFromMemory((ushort)(spriteaddr + 3));
                    byte usepal1 = (byte)((flags >> 4) & 0x1);
                    byte xflip = (byte)((flags >> 5) & 0x1);
                    byte yflip = (byte)((flags >> 6) & 0x1);
                    byte belowbg = (byte)((flags >> 7) & 0x1);
                    //byte c_palnr = (byte)(flags & 0x7);
                    //u8 c_vram1 = (flags >> 3) & 0x1;

                    byte line = GetValue(LY);
                    if (line < spritey || line >= (spritey + gpu_sprite_size)) { continue; }
                    if (spritex < -7 || spritex >= 160) { continue; }

                    ushort tiley = (ushort)(yflip == 0x01? (gpu_sprite_size - 1 - (line - spritey)) : (line - spritey));
                    ushort tileaddress = (ushort)(0x8000 + tilenum * 16 + tiley * 2);
                    ushort b0 = tileaddress;
                    ushort b1 = (ushort)(tileaddress + 1);
                    byte data0 = gbCPU.GetValueFromMemory(b0);
                    byte data1 = gbCPU.GetValueFromMemory(b1);

                    uint screen_off = (uint)(line * 160 );

                    for (byte x = 0; x < 8; x++)
                    {
                        if (((spritex + x) < 0) || ((spritex + x) >= 160)) continue;
                        if (belowbg == 0x01 && BGprio[spritex + x] != 0) continue;
                        byte off = (byte)(xflip  == 0x01?  x: (7 - x));
                        byte pal = (usepal1 == 0x01) ? GetValue(OBJPAL1) : GetValue(OBJPAL0);
                        byte color0_idx = (byte)((data0 >> (off)) & 0x1);
                        byte color1_idx = (byte)((data1 >> (off)) & 0x1);
                        byte color_idx = (byte)(color0_idx + color1_idx * 2);
                        if (color_idx == 0) continue;
                        byte r = 0 , g= 0, b  = 0;
                        byte color = (byte)((pal >> (color_idx * 2)) & 0x3);
                        if (color == 0) { r = 255; g = 255; b = 255; }
                        if (color == 1) { r = 192; g = 192; b = 192; }
                        if (color == 2) { r = 96; g = 96; b = 96; }
                        if (color == 3) { r = 0; g = 0; b = 0; }
                        data[screen_off+(spritex+x)] = (uint)(r << 16 | g << 8 | b);
                    }
                }
            }
        }
    }
}
