using System;
using System.Collections.Generic;
using System.Text;

namespace GbTry.Machine
{
    public partial class LR35902
    {
        public class DelegateOP
        {
            public delegate string Operation(byte op,string opname);
            public Operation m_operation;
            private string opname;
            private byte op;
            public DelegateOP(byte op,string opname,Operation operation)
            {
                this.op = op;
                this.opname = opname;
                m_operation = operation;
            }

            public string execOpeation()
            {
               return m_operation(op,opname);  
            }
        }

        private GbCPU gbCPU;
        private Dictionary<byte, DelegateOP> executeOP;
        private Dictionary<byte, DelegateOP> executeCBOP;
        public void GetGbCPU(GbCPU gbCPU)
        {
            this.gbCPU = gbCPU;
            executeOP = new Dictionary<byte, DelegateOP>() {
                {0x00,new DelegateOP(0x00,"NOP ",O_Nop)},
                {0x01,new DelegateOP(0x01,"LD ",O_LD_wReg)},
                {0x02,new DelegateOP(0x02,"LD ",O_LD_a_A)},
                {0x03,new DelegateOP(0x03,"INC ",O_INC_wReg)},
                {0x04,new DelegateOP(0x04,"INC ",O_INC)},
                {0x05,new DelegateOP(0x05,"DEC ",O_DEC)},
                {0x06,new DelegateOP(0x06,"LD ",O_LD_d)},
                {0x07,new DelegateOP(0x07,"RLCA",RLCA)},
                {0x08,new DelegateOP(0x08,"LD ",LD_a_wReg)},
                {0x09,new DelegateOP(0x09,"ADD HL,",ADD_HL_wReg)},
                {0x0A,new DelegateOP(0x0A,"LD ",O_LD_d)},
                {0x0B,new DelegateOP(0x0B,"DEC ",O_DEC)},
                {0x0C,new DelegateOP(0x0C,"INC ",O_INC)},
                {0x0D,new DelegateOP(0x0D,"DEC ",O_DEC)},
                {0x0E,new DelegateOP(0x0E,"LD ",O_LD_d)},
                {0x0F,new DelegateOP(0x0F,"RRCA ",RRCA)},

                {0x10,new DelegateOP(0x10,"STOP ",O_Nop)},
                {0x11,new DelegateOP(0x11,"LD ",O_LD_wReg)},
                {0x12,new DelegateOP(0x12,"LD ",O_LD_a_A)},
                {0x13,new DelegateOP(0x13,"INC ",O_INC_wReg)},
                {0x14,new DelegateOP(0x14,"INC ",O_INC)},
                {0x15,new DelegateOP(0x15,"DEC ",O_DEC)},
                {0x16,new DelegateOP(0x16,"LD ",O_LD_d)},
                {0x17,new DelegateOP(0x17,"RLA",RLCA)},
                {0x18,new DelegateOP(0x18,"JR ",O_JR)},
                {0x19,new DelegateOP(0x19,"ADD HL,",ADD_HL_wReg)},
                {0x1A,new DelegateOP(0x1A,"LD ",O_LD_d)},
                {0x1B,new DelegateOP(0x1B,"DEC ",O_DEC)},
                {0x1C,new DelegateOP(0x1C,"INC ",O_INC)},
                {0x1D,new DelegateOP(0x1D,"DEC ",O_DEC)},
                {0x1E,new DelegateOP(0x1E,"LD ",O_LD_d)},
                {0x1F,new DelegateOP(0x1F,"RRA ",RRCA)},

                {0x20,new DelegateOP(0x20,"JR NZ, ",O_JR)},
                {0x21,new DelegateOP(0x21,"LD ",O_LD_wReg)},
                {0x22,new DelegateOP(0x22,"LD (HL+) ",O_LD_a_A)},
                {0x23,new DelegateOP(0x23,"INC ",O_INC_wReg)},
                {0x24,new DelegateOP(0x24,"INC ",O_INC)},
                {0x25,new DelegateOP(0x25,"DEC ",O_DEC)},
                {0x26,new DelegateOP(0x26,"LD ",O_LD_d)},
                {0x27,new DelegateOP(0x27,"DAA ",O_DAA)},
                {0x28,new DelegateOP(0x28,"JR ",O_JR)},
                {0x29,new DelegateOP(0x29,"ADD HL,",ADD_HL_wReg)},
                {0x2A,new DelegateOP(0x2A,"LD ",O_LD_d)},
                {0x2B,new DelegateOP(0x2B,"DEC ",O_DEC)},
                {0x2C,new DelegateOP(0x2C,"INC ",O_INC)},
                {0x2D,new DelegateOP(0x2D,"DEC ",O_DEC)},
                {0x2E,new DelegateOP(0x2E,"LD ",O_LD_d)},
                {0x2F,new DelegateOP(0x2F,"CPL ",O_CPL)},

                {0x30,new DelegateOP(0x30,"JR NC, ",O_JR)},
                {0x31,new DelegateOP(0x31,"LD ",O_LD_wReg)},
                {0x32,new DelegateOP(0x32,"LD (HL-) ",O_LD_a_A)},
                {0x33,new DelegateOP(0x33,"INC ",O_INC_wReg)},
                {0x34,new DelegateOP(0x34,"INC ",O_INC)},
                {0x35,new DelegateOP(0x35,"DEC ",O_DEC)},
                {0x36,new DelegateOP(0x36,"LD ",O_LD_d)},
                {0x37,new DelegateOP(0x37,"SCF ",O_SCF)},
                {0x38,new DelegateOP(0x38,"JR ",O_JR)},
                {0x39,new DelegateOP(0x39,"ADD HL,",ADD_HL_wReg)},
                {0x3A,new DelegateOP(0x3A,"LD ",O_LD_d)},
                {0x3B,new DelegateOP(0x3B,"DEC ",O_DEC)},
                {0x3C,new DelegateOP(0x3C,"INC ",O_INC)},
                {0x3D,new DelegateOP(0x3D,"DEC ",O_DEC)},
                {0x3E,new DelegateOP(0x3E,"LD ",O_LD_d)},
                {0x3F,new DelegateOP(0x3F,"CCF ",O_CCF)},

                {0x40,new DelegateOP(0x40,"LD B,B",O_LD_R_R)},
                {0x41,new DelegateOP(0x41,"LD B,C",O_LD_R_R)},
                {0x42,new DelegateOP(0x42,"LD B,D",O_LD_R_R)},
                {0x43,new DelegateOP(0x43,"LD B,E",O_LD_R_R)},
                {0x44,new DelegateOP(0x44,"LD B,H",O_LD_R_R)},
                {0x45,new DelegateOP(0x45,"LD B,L",O_LD_R_R)},
                {0x46,new DelegateOP(0x46,"LD B,",O_LD_R_R)},
                {0x47,new DelegateOP(0x47,"LD B,A",O_LD_R_R)},
                {0x48,new DelegateOP(0x48,"LD C,B",O_LD_R_R)},
                {0x49,new DelegateOP(0x49,"LD C,C",O_LD_R_R)},
                {0x4A,new DelegateOP(0x4A,"LD C,D",O_LD_R_R)},
                {0x4B,new DelegateOP(0x4B,"LD C,E",O_LD_R_R)},
                {0x4C,new DelegateOP(0x4C,"LD C,H",O_LD_R_R)},
                {0x4D,new DelegateOP(0x4D,"LD C,L",O_LD_R_R)},
                {0x4E,new DelegateOP(0x4E,"LD C,",O_LD_R_R)},
                {0x4F,new DelegateOP(0x4F,"LD C,A",O_LD_R_R)},

                {0x50,new DelegateOP(0x50,"LD D,B",O_LD_R_R)},
                {0x51,new DelegateOP(0x51,"LD D,C",O_LD_R_R)},
                {0x52,new DelegateOP(0x52,"LD D,D",O_LD_R_R)},
                {0x53,new DelegateOP(0x53,"LD D,E",O_LD_R_R)},
                {0x54,new DelegateOP(0x54,"LD D,H",O_LD_R_R)},
                {0x55,new DelegateOP(0x55,"LD D,L",O_LD_R_R)},
                {0x56,new DelegateOP(0x56,"LD D,",O_LD_R_R)},
                {0x57,new DelegateOP(0x57,"LD D,A",O_LD_R_R)},
                {0x58,new DelegateOP(0x58,"LD E,B",O_LD_R_R)},
                {0x59,new DelegateOP(0x59,"LD E,C",O_LD_R_R)},
                {0x5A,new DelegateOP(0x5A,"LD E,D",O_LD_R_R)},
                {0x5B,new DelegateOP(0x5B,"LD E,E",O_LD_R_R)},
                {0x5C,new DelegateOP(0x5C,"LD E,H",O_LD_R_R)},
                {0x5D,new DelegateOP(0x5D,"LD E,L",O_LD_R_R)},
                {0x5E,new DelegateOP(0x5E,"LD E,",O_LD_R_R)},
                {0x5F,new DelegateOP(0x5F,"LD E,A",O_LD_R_R)},

                {0x60,new DelegateOP(0x60,"LD H,B",O_LD_R_R)},
                {0x61,new DelegateOP(0x61,"LD H,C",O_LD_R_R)},
                {0x62,new DelegateOP(0x62,"LD H,D",O_LD_R_R)},
                {0x63,new DelegateOP(0x63,"LD H,E",O_LD_R_R)},
                {0x64,new DelegateOP(0x64,"LD H,H",O_LD_R_R)},
                {0x65,new DelegateOP(0x65,"LD H,L",O_LD_R_R)},
                {0x66,new DelegateOP(0x66,"LD H,",O_LD_R_R)},
                {0x67,new DelegateOP(0x67,"LD H,A",O_LD_R_R)},
                {0x68,new DelegateOP(0x68,"LD L,B",O_LD_R_R)},
                {0x69,new DelegateOP(0x69,"LD L,C",O_LD_R_R)},
                {0x6A,new DelegateOP(0x6A,"LD L,D",O_LD_R_R)},
                {0x6B,new DelegateOP(0x6B,"LD L,E",O_LD_R_R)},
                {0x6C,new DelegateOP(0x6C,"LD L,H",O_LD_R_R)},
                {0x6D,new DelegateOP(0x6D,"LD L,L",O_LD_R_R)},
                {0x6E,new DelegateOP(0x6E,"LD L,",O_LD_R_R)},
                {0x6F,new DelegateOP(0x6F,"LD L,A",O_LD_R_R)},

                {0x70,new DelegateOP(0x70,"LD ",O_LD_R_R)},
                {0x71,new DelegateOP(0x71,"LD ",O_LD_R_R)},
                {0x72,new DelegateOP(0x72,"LD ",O_LD_R_R)},
                {0x73,new DelegateOP(0x73,"LD ",O_LD_R_R)},
                {0x74,new DelegateOP(0x74,"LD ",O_LD_R_R)},
                {0x75,new DelegateOP(0x75,"LD ",O_LD_R_R)},
                {0x76,new DelegateOP(0x76,"HALT",O_LD_R_R)},
                {0x77,new DelegateOP(0x77,"LD ",O_LD_R_R)},
                {0x78,new DelegateOP(0x78,"LD A,B",O_LD_R_R)},
                {0x79,new DelegateOP(0x79,"LD A,C",O_LD_R_R)},
                {0x7A,new DelegateOP(0x7A,"LD A,D",O_LD_R_R)},
                {0x7B,new DelegateOP(0x7B,"LD A,E",O_LD_R_R)},
                {0x7C,new DelegateOP(0x7C,"LD A,H",O_LD_R_R)},
                {0x7D,new DelegateOP(0x7D,"LD A,L",O_LD_R_R)},
                {0x7E,new DelegateOP(0x7E,"LD A,",O_LD_R_R)},
                {0x7F,new DelegateOP(0x7F,"LD A,A",O_LD_R_R)},

                {0x80,new DelegateOP(0x80,"ADD ",O_ADD)},
                {0x81,new DelegateOP(0x81,"ADD ",O_ADD)},
                {0x82,new DelegateOP(0x82,"ADD ",O_ADD)},
                {0x83,new DelegateOP(0x83,"ADD ",O_ADD)},
                {0x84,new DelegateOP(0x84,"ADD ",O_ADD)},
                {0x85,new DelegateOP(0x85,"ADD ",O_ADD)},
                {0x86,new DelegateOP(0x86,"ADD ",O_ADD)},
                {0x87,new DelegateOP(0x87,"ADD ",O_ADD)},
                {0x88,new DelegateOP(0x88,"ADC ",O_ADC)},
                {0x89,new DelegateOP(0x89,"ADC ",O_ADC)},
                {0x8A,new DelegateOP(0x8A,"ADC ",O_ADC)},
                {0x8B,new DelegateOP(0x8B,"ADC ",O_ADC)},
                {0x8C,new DelegateOP(0x8C,"ADC ",O_ADC)},
                {0x8D,new DelegateOP(0x8D,"ADC ",O_ADC)},
                {0x8E,new DelegateOP(0x8E,"ADC ",O_ADC)},
                {0x8F,new DelegateOP(0x8F,"ADC ",O_ADC)},

                {0x90,new DelegateOP(0x90,"SUB ",O_SUB)},
                {0x91,new DelegateOP(0x91,"SUB ",O_SUB)},
                {0x92,new DelegateOP(0x92,"SUB ",O_SUB)},
                {0x93,new DelegateOP(0x93,"SUB ",O_SUB)},
                {0x94,new DelegateOP(0x94,"SUB ",O_SUB)},
                {0x95,new DelegateOP(0x95,"SUB ",O_SUB)},
                {0x96,new DelegateOP(0x96,"SUB ",O_SUB)},
                {0x97,new DelegateOP(0x97,"SUB ",O_SUB)},
                {0x98,new DelegateOP(0x98,"SBC ",O_SBC)},
                {0x99,new DelegateOP(0x99,"SBC ",O_SBC)},
                {0x9A,new DelegateOP(0x9A,"SBC ",O_SBC)},
                {0x9B,new DelegateOP(0x9B,"SBC ",O_SBC)},
                {0x9C,new DelegateOP(0x9C,"SBC ",O_SBC)},
                {0x9D,new DelegateOP(0x9D,"SBC ",O_SBC)},
                {0x9E,new DelegateOP(0x9E,"SBC ",O_SBC)},
                {0x9F,new DelegateOP(0x9F,"SBC ",O_SBC)},

                {0xa0,new DelegateOP(0xa0,"AND ",O_AND)},
                {0xa1,new DelegateOP(0xa1,"AND ",O_AND)},
                {0xa2,new DelegateOP(0xa2,"AND ",O_AND)},
                {0xa3,new DelegateOP(0xa3,"AND ",O_AND)},
                {0xa4,new DelegateOP(0xa4,"AND ",O_AND)},
                {0xa5,new DelegateOP(0xa5,"AND ",O_AND)},
                {0xa6,new DelegateOP(0xa6,"AND ",O_AND)},
                {0xa7,new DelegateOP(0xa7,"AND ",O_AND)},
                {0xa8,new DelegateOP(0xa8,"XOR ",O_XOR)},
                {0xa9,new DelegateOP(0xa9,"XOR ",O_XOR)},
                {0xaA,new DelegateOP(0xaA,"XOR ",O_XOR)},
                {0xaB,new DelegateOP(0xaB,"XOR ",O_XOR)},
                {0xaC,new DelegateOP(0xaC,"XOR ",O_XOR)},
                {0xaD,new DelegateOP(0xaD,"XOR ",O_XOR)},
                {0xaE,new DelegateOP(0xaE,"XOR ",O_XOR)},
                {0xaF,new DelegateOP(0xaF,"XOR ",O_XOR)},

                {0xb0,new DelegateOP(0xb0,"OR ",O_OR)},
                {0xb1,new DelegateOP(0xb1,"OR ",O_OR)},
                {0xb2,new DelegateOP(0xb2,"OR ",O_OR)},
                {0xb3,new DelegateOP(0xb3,"OR ",O_OR)},
                {0xb4,new DelegateOP(0xb4,"OR ",O_OR)},
                {0xb5,new DelegateOP(0xb5,"OR ",O_OR)},
                {0xb6,new DelegateOP(0xb6,"OR ",O_OR)},
                {0xb7,new DelegateOP(0xb7,"OR ",O_OR)},
                {0xb8,new DelegateOP(0xb8,"CP ",O_CP)},
                {0xb9,new DelegateOP(0xb9,"CP ",O_CP)},
                {0xbA,new DelegateOP(0xbA,"CP ",O_CP)},
                {0xbB,new DelegateOP(0xbB,"CP ",O_CP)},
                {0xbC,new DelegateOP(0xbC,"CP ",O_CP)},
                {0xbD,new DelegateOP(0xbD,"CP ",O_CP)},
                {0xbE,new DelegateOP(0xbE,"CP ",O_CP)},
                {0xbF,new DelegateOP(0xbF,"CP ",O_CP)},

                {0xc0,new DelegateOP(0xc0,"RET ",O_RET)},
                {0xc1,new DelegateOP(0xc1,"POP ",O_POP)},
                {0xc2,new DelegateOP(0xc2,"JP ",O_JP)},
                {0xc3,new DelegateOP(0xc3,"JP ",O_JP)},
                {0xc4,new DelegateOP(0xc4,"CALL ",O_CALL)},
                {0xc5,new DelegateOP(0xc5,"PUSH ",O_PUSH)},
                {0xc6,new DelegateOP(0xc6,"ADD ",O_ADD)},
                {0xc7,new DelegateOP(0xc7,"RST ",O_RST)},
                {0xc8,new DelegateOP(0xc8,"RET ",O_RET)},
                {0xc9,new DelegateOP(0xc9,"RET ",O_RET)},
                {0xcA,new DelegateOP(0xcA,"JP ",O_JP)},
                {0xcB,new DelegateOP(0xcB,"NOP ",O_Nop)},
                {0xcC,new DelegateOP(0xcC,"CALL ",O_CALL)},
                {0xcD,new DelegateOP(0xcD,"CALL ",O_CALL)},
                {0xcE,new DelegateOP(0xcE,"ADC ",O_ADC)},
                {0xcF,new DelegateOP(0xcF,"RST ",O_RST)},

                {0xd0,new DelegateOP(0xd0,"RET ",O_RET)},
                {0xd1,new DelegateOP(0xd1,"POP ",O_POP)},
                {0xd2,new DelegateOP(0xd2,"JP ",O_JP)},
                {0xd3,new DelegateOP(0xd3,"NOP ",O_Nop)},
                {0xd4,new DelegateOP(0xd4,"CALL ",O_CALL)},
                {0xd5,new DelegateOP(0xd5,"PUSH ",O_PUSH)},
                {0xd6,new DelegateOP(0xd6,"SUB ",O_SUB)},
                {0xd7,new DelegateOP(0xd7,"RST ",O_RST)},
                {0xd8,new DelegateOP(0xd8,"RET ",O_RET)},
                {0xd9,new DelegateOP(0xd9,"RET ",O_RET)},
                {0xdA,new DelegateOP(0xdA,"JP ",O_JP)},
                {0xdB,new DelegateOP(0xdB,"NOP ",O_Nop)},
                {0xdC,new DelegateOP(0xdC,"CALL ",O_CALL)},
                {0xdD,new DelegateOP(0xdD,"NOP ",O_Nop)},
                {0xdE,new DelegateOP(0xdE,"SBC ",O_SBC)},
                {0xdF,new DelegateOP(0xdF,"RST ",O_RST)},

                {0xe0,new DelegateOP(0xe0,"LDH ",O_LD_a_A)},
                {0xe1,new DelegateOP(0xe1,"POP ",O_POP)},
                {0xe2,new DelegateOP(0xe2,"LD ",O_LD_a_A)},
                {0xe3,new DelegateOP(0xe3,"NOP ",O_Nop)},
                {0xe4,new DelegateOP(0xe4,"NOP ",O_Nop)},
                {0xe5,new DelegateOP(0xe5,"PUSH ",O_PUSH)},
                {0xe6,new DelegateOP(0xe6,"AND ",O_AND)},
                {0xe7,new DelegateOP(0xe7,"RST ",O_RST)},
                {0xe8,new DelegateOP(0xe8,"ADD ",O_ADD)},
                {0xe9,new DelegateOP(0xe9,"JP ",O_JP)},
                {0xeA,new DelegateOP(0xeA,"LD ",O_LD_a_A)},
                {0xeB,new DelegateOP(0xeB,"NOP ",O_Nop)},
                {0xeC,new DelegateOP(0xeC,"NOP ",O_Nop)},
                {0xeD,new DelegateOP(0xeD,"NOP ",O_Nop)},
                {0xeE,new DelegateOP(0xeE,"XOR ",O_XOR)},
                {0xeF,new DelegateOP(0xeF,"RST ",O_RST)},

                {0xf0,new DelegateOP(0xf0,"LDH ",O_LD_a_A)},
                {0xf1,new DelegateOP(0xf1,"POP ",O_POP)},
                {0xf2,new DelegateOP(0xf2,"LD ",O_LD_a_A)},
                {0xf3,new DelegateOP(0xf3,"DI ",O_Nop)},
                {0xf4,new DelegateOP(0xf4,"NOP ",O_Nop)},
                {0xf5,new DelegateOP(0xf5,"PUSH ",O_PUSH)},
                {0xf6,new DelegateOP(0xf6,"OR ",O_OR)},
                {0xf7,new DelegateOP(0xf7,"RST ",O_RST)},
                {0xf8,new DelegateOP(0xf8,"LD ",O_LD_a_A)},
                {0xf9,new DelegateOP(0xf9,"LD ",O_LD_a_A)},
                {0xfA,new DelegateOP(0xfA,"LD ",O_LD_a_A)},
                {0xfB,new DelegateOP(0xfB,"EI ",O_Nop)},
                {0xfC,new DelegateOP(0xfC,"NOP ",O_Nop)},
                {0xfD,new DelegateOP(0xfD,"NOP ",O_Nop)},
                {0xfE,new DelegateOP(0xfE,"CP ",O_CP)},
                {0xfF,new DelegateOP(0xfF,"RST ",O_RST)},
            };
            Init_CB_exeucteOperation();
        }

        public void ExecuteOP(byte op)
        {
            if (op == 0xCB)
            {
                op = gbCPU.fetch();
                gbCPU.commandstring = executeCBOP[op].execOpeation();
                return;
            }
            gbCPU.commandstring = executeOP[op].execOpeation();
        }
        public string O_RST(byte op, string opname)
        {
            switch (op)
            {
                case 0xc7:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0;
                    if (gbCPU.debugflag)
                        opname += "00H";
                    break;
                case 0xcf:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x08;
                    if (gbCPU.debugflag)
                        opname += "08H";
                    break;
                case 0xd7:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x10;
                    if (gbCPU.debugflag)
                        opname += "10H";
                    break;
                case 0xdf:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x18;
                    if (gbCPU.debugflag)
                        opname += "18H";
                    break;
                case 0xe7:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x20;
                    if (gbCPU.debugflag)
                        opname += "20H";
                    break;
                case 0xef:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x28;
                    if (gbCPU.debugflag)
                        opname += "28H";
                    break;
                case 0xf7:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x30;
                    if (gbCPU.debugflag)
                        opname += "30H";
                    break;
                case 0xff:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x38;
                    if (gbCPU.debugflag)
                        opname += "38H";
                    break;
                case 0x40:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x40;
                    gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += "40H";
                    break;
                case 0x48:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x48;
                    gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += "48H";
                    break;
                case 0x50:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x50;
                    gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += "50H";
                    break;
                case 0x60:
                    gbCPU.PUSH(gbCPU.PC.word);
                    gbCPU.PC.word = 0x60;
                    gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += "60H";
                    break;
            }
            gbCPU.IncCycle(16);
            return opname;
        }

        public string O_PUSH(byte op, string opname)
        {
            switch (op)
            {
                case 0xc5:
                    gbCPU.PUSH(gbCPU.BC.word);
                    if (gbCPU.debugflag)
                        opname += "BC";
                    break;
                case 0xd5:
                    gbCPU.PUSH(gbCPU.DE.word);
                    if (gbCPU.debugflag)
                        opname += "DE";
                    break;
                case 0xe5:
                    gbCPU.PUSH(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += "HL";
                    break;
                case 0xf5:
                    gbCPU.PUSH(gbCPU.AF.word);
                    if (gbCPU.debugflag)
                        opname += "AF";
                    break;
            }
            gbCPU.IncCycle(16);
            return opname;
        }
        public string O_CALL(byte op, string opname)
        {
            switch (op)
            {
                case 0xc4:
                    { 
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagZ() == 0x00)
                        {
                            gbCPU.PUSH(gbCPU.PC.word);
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(12);
                        }
                        if (gbCPU.debugflag)
                            opname += "NZ,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xcc:
                    { 
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagZ() == 0x01)
                        {
                            gbCPU.PUSH(gbCPU.PC.word);
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(12);
                        }
                        if (gbCPU.debugflag)
                            opname += "Z,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xcd:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        gbCPU.PUSH(gbCPU.PC.word);
                        gbCPU.PC.word = value;
                        gbCPU.IncCycle(12);
                        if (gbCPU.debugflag)
                            opname += "$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xd4:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagC() == 0x00)
                        {
                            gbCPU.PUSH(gbCPU.PC.word);
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(12);
                        }
                        if (gbCPU.debugflag)
                            opname += "NC,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xdc:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagC() == 0x01)
                        {
                            gbCPU.PUSH(gbCPU.PC.word);
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(12);
                        }
                        if (gbCPU.debugflag)
                            opname += "NC,$" + value.ToString("X") + "h";
                        break;
                    }
            }
            gbCPU.IncCycle(12);
            return opname;
        }

        public string O_JP(byte op, string opname)
        {
            switch (op)
            {
                case 0xc2:
                    { 
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if(gbCPU.GetFlagZ() == 0x00)
                        {
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(4);
                        }
                        if (gbCPU.debugflag)
                            opname += "NZ,$"+ value.ToString("X")+"h";
                        break;
                    }
                case 0xc3:
                    gbCPU.PC.word = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.PC.word.ToString("X") + "h";
                    break;
                case 0xca:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagZ() == 0x01)
                        {
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(4);
                        }
                        if (gbCPU.debugflag)
                            opname += "Z,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xd2:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagC() == 0x00)
                        {
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(4);
                        }
                        if (gbCPU.debugflag)
                            opname += "NC,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xda:
                    {
                        var value = (ushort)(gbCPU.fetch() | (gbCPU.fetch() << 8));
                        if (gbCPU.GetFlagC() == 0x01)
                        {
                            gbCPU.PC.word = value;
                            gbCPU.IncCycle(4);
                        }
                        if (gbCPU.debugflag)
                            opname += "C,$" + value.ToString("X") + "h";
                        break;
                    }
                case 0xe9:
                    {
                        gbCPU.PC.word = gbCPU.HL.word;
                        gbCPU.IncCycle(4);
                        if (gbCPU.debugflag)
                            opname += "$" + gbCPU.HL.word.ToString("X") + "h";
                        return opname;
                    }
            }
            gbCPU.IncCycle(12);
            return opname;
        }

        public string O_POP(byte op, string opname)
        {
            switch (op)
            {
                case 0xc1:
                    gbCPU.BC.word = gbCPU.POP();
                    if (gbCPU.debugflag)
                        opname += "BC";
                    break;
                case 0xd1:
                    gbCPU.DE.word = gbCPU.POP();
                    if (gbCPU.debugflag)
                        opname += "DE";
                    break;
                case 0xe1:
                    gbCPU.HL.word = gbCPU.POP();
                    if (gbCPU.debugflag)
                        opname += "HL";
                    break;
                case 0xf1:
                    var word = gbCPU.POP();
                    gbCPU.AF.lowbyte = (byte)(word >> 0x08);
                    gbCPU.AF.highbyte = (byte)(word & 0x00F0);
                    if (gbCPU.debugflag)
                        opname += "AF";
                    break;
            }
            gbCPU.IncCycle(12);
            return opname;
        }
        public string O_RET(byte op, string opname)
        {
            switch (op)
            {
                case 0xc0:
                    if (gbCPU.GetFlagZ() == 0x00)
                    {
                        gbCPU.PC.word = gbCPU.POP();
                        if (gbCPU.debugflag)
                            opname += "NZ";
                        gbCPU.IncCycle(12);
                    }
                    break;
                case 0xc8:
                    if (gbCPU.GetFlagZ() == 0x01)
                    {
                        gbCPU.PC.word = gbCPU.POP();
                        if (gbCPU.debugflag)
                            opname += "Z";
                        gbCPU.IncCycle(12);
                    }
                    break;
                case 0xc9:
                    gbCPU.PC.word = gbCPU.POP();
                    gbCPU.IncCycle(8);
                    break;
                case 0xd0:
                    if (gbCPU.GetFlagC() == 0x00)
                    {
                        gbCPU.PC.word = gbCPU.POP();
                        if (gbCPU.debugflag)
                            opname += "NC";
                        gbCPU.IncCycle(12);
                    }
                    break;
                case 0xd8:
                    if (gbCPU.GetFlagC() == 0x01)
                    {
                        gbCPU.PC.word = gbCPU.POP();
                        if (gbCPU.debugflag)
                            opname += "C";
                        gbCPU.IncCycle(12);
                    }
                    break;
                case 0xd9:
                    gbCPU.PC.word = gbCPU.POP();
                    gbCPU.EI();
                    gbCPU.IncCycle(8);
                    break;
            }
            gbCPU.IncCycle(8);
            return opname;
        }

        public string O_Nop(byte op, string opname)
        {
            //NOP
            switch (op)
            {
                case 0x10:
                    gbCPU.fetch();
                    opname += op.ToString("X");
                    break;
                case 0xcb:
                case 0xd3:
                case 0xdb:
                case 0xdd:
                case 0xe3:
                case 0xe4:
                case 0xeb:
                case 0xec:
                case 0xed:
                case 0xf4:
                case 0xfc:
                case 0xfd:
                    opname += op.ToString("X");
                    return opname;
                case 0xf3:
                    gbCPU.DI();
                    break;
                case 0xfb:
                    gbCPU.EI();
                    break;
            }
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_SCF(byte op, string opname)
        {
            gbCPU.SetFlagC(true);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_CCF(byte op, string opname)
        {
            gbCPU.SetFlagC(gbCPU.GetFlagC() == 0);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_CPL(byte op, string opname)
        {
            gbCPU.AF.lowbyte = (byte)~gbCPU.AF.lowbyte;
            gbCPU.SetFlagH(true);
            gbCPU.SetFlagN(true);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_DAA(byte op, string opname)
        {
            if (gbCPU.GetFlagN() == 0)
            {    
                if (gbCPU.GetFlagC() == 0x01 || (gbCPU.AF.lowbyte > 0x99))
                {   
                    gbCPU.AF.lowbyte += 0x60;
                    gbCPU.SetFlagC(true);
                }
                if (gbCPU.GetFlagH() == 0x01 || ((gbCPU.AF.lowbyte & 0xF) > 9))
                {
                    gbCPU.AF.lowbyte += 0x06;
                }
            }
            else
            {    
                if (gbCPU.GetFlagC() == 0x01)
                {
                    gbCPU.AF.lowbyte -= 0x60;
                    gbCPU.SetFlagC(true);
                }
                if (gbCPU.GetFlagH() == 0x01)
                {
                    gbCPU.AF.lowbyte -= 0x06;
                }
            }
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagZ(gbCPU.AF.lowbyte == 0);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_LD_wReg(byte op, string opname)
        {

            switch (op)
            {
                case 0x01:            //LD BC d16
                    gbCPU.BC.word = ((ushort)(gbCPU.fetch() |  gbCPU.fetch()<< 0x08));
                    if (gbCPU.debugflag)
                        opname += " BC,"+gbCPU.BC.word.ToString("X")+"h";
                    break;
                case 0x11:            //LD DE d16
                    gbCPU.DE.word = ((ushort)(gbCPU.fetch() | gbCPU.fetch() << 0x08));
                    if (gbCPU.debugflag)
                        opname += " DE," + gbCPU.DE.word.ToString("X") + "h";
                    break;
                case 0x21:            //LD HL d16
                    gbCPU.HL.word = ((ushort)(gbCPU.fetch() | gbCPU.fetch() << 0x08));
                    if (gbCPU.debugflag)
                        opname += " HL," + gbCPU.HL.word.ToString("X") + "h";
                    break;
                case 0x31:            //LD SP d16
                    gbCPU.SP.word = ((ushort)(gbCPU.fetch() | gbCPU.fetch() << 0x08));
                    if (gbCPU.debugflag)
                        opname += " SP," + gbCPU.SP.word.ToString("X") + "h";
                    break;
            }
            gbCPU.IncCycle(12);
            return opname;
        }

        public string O_LD_a_A(byte op, string opname)
        {
            
            switch (op)
            {
                case 0x02: //LD (BC) A
                    gbCPU.SetValueIntoMemory(gbCPU.BC.word, gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$"+gbCPU.BC.word.ToString("X")+","+ gbCPU.AF.lowbyte.ToString("X") + "h";
                    break;
                case 0x12: //LD (DE) A
                    gbCPU.SetValueIntoMemory(gbCPU.DE.word, gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.DE.word.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                    break;
                case 0x22: //LD (HL+) A
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                    gbCPU.HL.word++;
                    break;
                case 0x32: //LD (HL-) A
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                    gbCPU.HL.word--;
                    break;
                case 0xe0: //LDH (a8) A
                    {
                        var value = (ushort)(0xFF00 | gbCPU.fetch());
                        gbCPU.SetValueIntoMemory(value, gbCPU.AF.lowbyte);
                        if (gbCPU.debugflag)
                            opname += "$" + value.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                        gbCPU.IncCycle(4);
                        break;
                    }
                case 0xe2: //LD (c) A
                    {
                        var value = (ushort)(0xFF00 | gbCPU.BC.highbyte);
                        gbCPU.SetValueIntoMemory(value, gbCPU.AF.lowbyte);
                        if (gbCPU.debugflag)
                            opname += "$" + value.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                        break;
                    }
                case 0xea: //LD (a16) A
                    {
                        var value = (ushort)(gbCPU.fetch() | gbCPU.fetch()<<8);
                        gbCPU.SetValueIntoMemory(value, gbCPU.AF.lowbyte);
                        if (gbCPU.debugflag)
                            opname += "$" + value.ToString("X") + "," + gbCPU.AF.lowbyte.ToString("X") + "h";
                        break;
                    }
                case 0xf0: //LDH A,(a8)
                    {
                        var value = (ushort)(0xFF00 | gbCPU.fetch());
                        gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(value);
                        if (gbCPU.debugflag)
                            opname += "A,$" + value.ToString("X");
                        gbCPU.IncCycle(4);
                        break;
                    }
                case 0xf2: //LD  A,(c)
                    {
                        var value = (ushort)(0xFF00 | gbCPU.BC.highbyte);
                        gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(value);
                        if (gbCPU.debugflag)
                            opname += "A,$" + value.ToString("X");
                        break;
                    }
                case 0xf8: //LD  HL,(c)
                    {
                        sbyte value = (sbyte)gbCPU.fetch();
                        int result = value + gbCPU.SP.word;
                        gbCPU.SetFlagC(((gbCPU.SP.word ^ value ^ result) & 0x100) == 0x100);
                        gbCPU.SetFlagH(((gbCPU.SP.word ^ value ^ result) & 0x10) == 0x10);
                        gbCPU.SetFlagN(false);
                        gbCPU.SetFlagZ(false);
                        gbCPU.HL.word = (ushort)result;
                        if (gbCPU.debugflag)
                            opname += "HL,$" + value.ToString("X");
                        gbCPU.IncCycle(4);
                        break;
                    }
                case 0xf9: //LD SP,HL
                    {
                        gbCPU.SP.word = gbCPU.HL.word;
                        if (gbCPU.debugflag)
                            opname += "SP,HL";
                        break;
                    }
                case 0xfa: //LD A,(a16) 
                    {
                        var value = (ushort)(gbCPU.fetch() | gbCPU.fetch() << 8);
                        gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(value);
                        if (gbCPU.debugflag)
                            opname += "A,$" + value.ToString("X");
                        gbCPU.IncCycle(8);
                        break;
                    }
            }
            gbCPU.IncCycle(8);
            return opname;
        }

        public string O_INC_wReg(byte op, string opname)
        {
            switch(op)
            {
                case 0x03://INC BC
                    gbCPU.BC.word++;  
                    if (gbCPU.debugflag)
                        opname += "BC";
                    break;
                case 0x13://INC DE
                    gbCPU.DE.word++;
                    if (gbCPU.debugflag)
                        opname += "DE";
                    break;
                case 0x23://INC HL
                    gbCPU.HL.word++;
                    if (gbCPU.debugflag)
                        opname += "HL";
                    break;
                case 0x33://INC SP
                    gbCPU.SP.word++;
                    if (gbCPU.debugflag)
                        opname += "SP";
                    break;
            }
            gbCPU.IncCycle(8);
            return opname;
        }

        public string O_INC(byte op, string opname)
        {
            //INC ?
            switch (op)
            {
                case 0x04:  //B
                    gbCPU.BC.lowbyte = CalcINC(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "B";
                    break;
                case 0x0C:  //C
                    gbCPU.BC.highbyte = CalcINC(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "C";
                    break;
                case 0x14:  //D
                    gbCPU.DE.lowbyte = CalcINC(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "D";
                    break;
                case 0x1C:  //E
                    gbCPU.DE.highbyte = CalcINC(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "E";
                    break;
                case 0x24:  //H
                    gbCPU.HL.lowbyte = CalcINC(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "H";
                    break;
                case 0x2C:  //L
                    gbCPU.HL.highbyte = CalcINC(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "L";
                    break;
                case 0x34:  //(HL)
                    var value = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, CalcINC(value));
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$" + gbCPU.HL.word.ToString("X") + "," + value.ToString("X") + "h";
                    break;
                case 0x3C:  //A
                    gbCPU.AF.lowbyte = CalcINC(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A";
                    break;
            }

            return opname;
        }
        private byte CalcINC(byte retvalue)
        {
            retvalue++;
            gbCPU.SetFlagN(false);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagH((retvalue&0x0F) == 0x00); 
            gbCPU.IncCycle(4);
            return retvalue;
        }

        public string O_DEC(byte op, string opname)
        {
            //DEC ?
            switch (op)
            {
                case 0x05:  //B
                    gbCPU.BC.lowbyte = CalcDEC(gbCPU.BC.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "B";
                    break;
                case 0x0B:  //BC
                    gbCPU.BC.word--;
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "BC";
                    break;
                case 0x0D:  //C
                    gbCPU.BC.highbyte = CalcDEC(gbCPU.BC.highbyte);
                    if (gbCPU.debugflag)
                        opname += "C";
                    break;
                case 0x15:  //D
                    gbCPU.DE.lowbyte = CalcDEC(gbCPU.DE.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "D";
                    break;
                case 0x1B:  //DE
                    gbCPU.DE.word--;
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "DE";
                    break;
                case 0x1D:  //E
                    gbCPU.DE.highbyte = CalcDEC(gbCPU.DE.highbyte);
                    if (gbCPU.debugflag)
                        opname += "E";
                    break;
                case 0x25:  //H
                    gbCPU.HL.lowbyte = CalcDEC(gbCPU.HL.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "H";
                    break;
                case 0x2B:  //HL
                    gbCPU.HL.word--;
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "HL";
                    break;
                case 0x2D:  //L
                    gbCPU.HL.highbyte = CalcDEC(gbCPU.HL.highbyte);
                    if (gbCPU.debugflag)
                        opname += "L";
                    break;
                case 0x35:  //(HL)
                    var value = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word,CalcDEC(value));
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "$"+ gbCPU.HL.word.ToString("X")+","+ value.ToString("X") + "h";
                    break;
                case 0x3B:  //SP
                    gbCPU.SP.word--;
                    gbCPU.IncCycle(8);
                    if (gbCPU.debugflag)
                        opname += "SP";
                    break;
                case 0x3D:  //A
                    gbCPU.AF.lowbyte = CalcDEC(gbCPU.AF.lowbyte);
                    if (gbCPU.debugflag)
                        opname += "A";
                    break;
            }

            return opname;
        }
        private byte CalcDEC(byte retvalue)
        {
            retvalue--;
            gbCPU.SetFlagN(true);
            gbCPU.SetFlagZ(retvalue == 0x00);
            gbCPU.SetFlagH((retvalue & 0x0F) == 0x0F);
            gbCPU.IncCycle(4);
            return retvalue;
        }

        public string O_LD_d(byte op, string opname)
        {
            //LD ?
            switch (op)
            {
                case 0x06:  //B, d8
                    gbCPU.BC.lowbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " B," + gbCPU.BC.lowbyte.ToString("X") + "h";
                    break;
                case 0x0A: // A,(BC)
                    gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(gbCPU.BC.word);
                    if (gbCPU.debugflag)
                        opname += " A,$" + gbCPU.BC.word.ToString("X") + "h";
                    break;
                case 0x0E:  //C, d8
                    gbCPU.BC.highbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " C," + gbCPU.BC.highbyte.ToString("X") + "h";
                    break;
                case 0x16:  //D, d8
                    gbCPU.DE.lowbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " D," + gbCPU.DE.lowbyte.ToString("X") + "h";
                    break;
                case 0x1A: // A,(DE)
                    gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(gbCPU.DE.word);
                    if (gbCPU.debugflag)
                        opname += " A,$" + gbCPU.DE.word.ToString("X") + "h";
                    break;
                case 0x1E:  //E, d8
                    gbCPU.DE.highbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " E," + gbCPU.DE.highbyte.ToString("X") + "h";
                    break;
                case 0x26:  //H, d8
                    gbCPU.HL.lowbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " H," + gbCPU.HL.lowbyte.ToString("X") + "h";
                    break;
                case 0x2A: // A,(HL+)
                    gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " A,$" + gbCPU.HL.word.ToString("X") + "h";
                    gbCPU.HL.word++;
                    break;
                case 0x2E:  //L, d8
                    gbCPU.HL.highbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " L," + gbCPU.HL.highbyte.ToString("X") + "h";
                    break;
                case 0x36:  //(HL), d8
                    var value = gbCPU.fetch();
                    gbCPU.SetValueIntoMemory(gbCPU.HL.word, value);
                    gbCPU.IncCycle(4);
                    if (gbCPU.debugflag)
                        opname += " $" + gbCPU.HL.word.ToString("X")+","+ value.ToString("X") + "h";
                    break;
                case 0x3A: // A,(HL-)
                    gbCPU.AF.lowbyte = gbCPU.GetValueFromMemory(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += " A,$" + gbCPU.HL.word.ToString("X") + "h";
                    gbCPU.HL.word--;
                    break;
                case 0x3E:  //A, d8
                    gbCPU.AF.lowbyte = gbCPU.fetch();
                    if (gbCPU.debugflag)
                        opname += " A," + gbCPU.AF.lowbyte.ToString("X") + "h";
                    break;
            }
            gbCPU.IncCycle(8);
            return opname;
        }

        public string RLCA(byte op, string opname)
        {
            switch(op)
            {
                case 0x07:
                    {
                        var Carry = (byte)((gbCPU.AF.lowbyte >> 0x07) & 0x01);
                        gbCPU.AF.lowbyte = (byte)(Carry | (gbCPU.AF.lowbyte << 0x01));
                        gbCPU.SetFlagC(Carry == 0x01);
                        break;
                    }
                case 0x17:
                    {
                        var Carry = gbCPU.GetFlagC();
                        gbCPU.SetFlagC((byte)((gbCPU.AF.lowbyte >> 0x07) & 0x01) == 0x01); 
                        gbCPU.AF.lowbyte = (byte)(Carry | (gbCPU.AF.lowbyte << 0x01));
                        break;
                    }
            }
            gbCPU.SetFlagZ(false);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string RRCA(byte op, string opname)
        {
            switch (op)
            {
                case 0x0f:
                    {
                        var Carry = (byte)(gbCPU.AF.lowbyte & 0x01);
                        gbCPU.AF.lowbyte = (byte)(Carry << 0x07 | (gbCPU.AF.lowbyte >> 0x01));
                        gbCPU.SetFlagC(Carry == 0x01);
                    }
                    break;
                case 0x1f:
                    {
                        var Carry = gbCPU.GetFlagC();
                        gbCPU.SetFlagC((byte)(gbCPU.AF.lowbyte & 0x01) == 0x01); 
                        gbCPU.AF.lowbyte = (byte)(Carry << 0x07 | (gbCPU.AF.lowbyte >> 0x01));
                    }
                    break;
            }

            gbCPU.SetFlagZ(false);
            gbCPU.SetFlagH(false);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(4);
            return opname;
        }

        public string O_JR(byte op, string opname)
        {
            switch (op)
            {
                case 0x18:
                    {
                        sbyte data = (sbyte)gbCPU.fetch();
                        gbCPU.PC.word = (ushort)(gbCPU.PC.word + data); ;
                        gbCPU.IncCycle(12);
                        if (gbCPU.debugflag)
                            opname += " $" + data.ToString("X") + "h";
                        break;
                    }
                case 0x20:
                    {
                        sbyte data = (sbyte)gbCPU.fetch();
                        if(gbCPU.GetFlagZ() == 0)
                        {
                            gbCPU.PC.word = (ushort)(gbCPU.PC.word + data);
                            gbCPU.IncCycle(12);
                        }
                        else{
                            gbCPU.IncCycle(8);
                        } 
                        if (gbCPU.debugflag)
                                opname += " $" + data.ToString("X") + "h";
                        break;
                    }
                case 0x28:
                    {
                        sbyte data = (sbyte)gbCPU.fetch();
                        if (gbCPU.GetFlagZ() == 1)
                        {
                            gbCPU.PC.word = (ushort)(gbCPU.PC.word + data); ;
                            gbCPU.IncCycle(12);
                        }
                        else
                        {
                            gbCPU.IncCycle(8);
                        }
                        if (gbCPU.debugflag)
                            opname += " $" + data.ToString("X") + "h";
                        break;
                    }
                case 0x30:
                    {
                        sbyte data = (sbyte)gbCPU.fetch();
                        if (gbCPU.GetFlagC() == 0)
                        {
                            gbCPU.PC.word = (ushort)(gbCPU.PC.word + data); ;
                            gbCPU.IncCycle(12);
                        }
                        else
                        {
                            gbCPU.IncCycle(8);
                        } 
                        if (gbCPU.debugflag)
                                opname += " $" + data.ToString("X") + "h";
                        break;
                    }
                case 0x38:
                    {
                        sbyte data = (sbyte)gbCPU.fetch();
                        if (gbCPU.GetFlagC() == 1)
                        {
                            gbCPU.PC.word = (ushort)(gbCPU.PC.word + data); ;
                            gbCPU.IncCycle(12);
                        }
                        else
                        {
                            gbCPU.IncCycle(8);
                        } 
                        if (gbCPU.debugflag)
                                opname += " $" + data.ToString("X") + "h";
                        break;
                    }
            }
            return opname;
        }


        public string LD_a_wReg(byte op, string opname)
        {
            byte l = gbCPU.fetch();
            ushort w = (ushort)(gbCPU.fetch() << 8 | l);
            switch (op)
            {
                case 0x08:
                    gbCPU.SetValueIntoMemory(w++,gbCPU.SP.highbyte);
                    gbCPU.SetValueIntoMemory(w, gbCPU.SP.lowbyte); 
                    if (gbCPU.debugflag)
                        opname += " $"+ (--w).ToString("X") + "h,SP";
                    break;
            }
            gbCPU.IncCycle(20);
            return opname;
        }

        public string ADD_HL_wReg(byte op, string opname)
        {
            switch (op)
            {
                case 0x09:
                    gbCPU.HL.word = CalcADDHL(gbCPU.BC.word);
                    if (gbCPU.debugflag)
                        opname += "BC";
                    break;
                case 0x19:
                    gbCPU.HL.word = CalcADDHL(gbCPU.DE.word);
                    if (gbCPU.debugflag)
                        opname += "DE";
                    break;
                case 0x29:
                    gbCPU.HL.word = CalcADDHL(gbCPU.HL.word);
                    if (gbCPU.debugflag)
                        opname += "HL";
                    break;
                case 0x39:
                    gbCPU.HL.word = CalcADDHL(gbCPU.SP.word);
                    if (gbCPU.debugflag)
                        opname += "SP";
                    break;
            }
            return opname;
        }
        public ushort CalcADDHL(ushort wReg)
        {
            int var = gbCPU.HL.word + wReg;
            gbCPU.SetFlagC(var > 0xFFFF);
            gbCPU.SetFlagH((gbCPU.HL.word & 0xFFF) + (wReg & 0xFFF) > 0xFFF);
            gbCPU.SetFlagN(false);
            gbCPU.IncCycle(8);
            return (ushort)var;
        }
           
    }
}
