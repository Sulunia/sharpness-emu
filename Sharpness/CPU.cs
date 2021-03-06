﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpness
{
    public class CPU
    {
        //Internals
        public event Action<string> LogExternal;

        // 6502 ---------------------------------------------
        // Acumulador
        private byte A;

        // Registradores
        private byte X;
        private byte Y;

        // Flags
        private byte P;

        // Stack Pointer
        private byte S;

        // Program Counter
        public ushort PC;

        // Memória Principal
        public byte[] mem = new byte[0x10000];

        // Flags
        private bool N, V, B, D, I, Z, C;

        // Auxiliar para decodificação do Opcode com mais de um argumento
        private ushort argop;

        public void Emulate()
        {
            if(LogExternal != null)
            {
                LogExternal("Opcode: " + mem[PC].ToString("X2"));
                LogExternal("PC: " + PC.ToString("X4"));
            }

            switch (mem[PC])
            {
                case 0x00:
                    //txtDebug.Text += "BRK - Break | Implicit \n";
                    PC += 1;
                    break;

                case 0x01:
                    //txtDebug.Text += "ORA - Bitwise OR with ACC | Indirect with X offset\n";
                    PC += 2;
                    break;

                case 0x08:
                    //txtDebug.Text += "PHP - Push Processor status (from stack?) | Implicit \n";
                    PC += 1;
                    break;

                case 0x09:
                    //txtDebug.Text += "ORA - Bitwise OR with ACC | Relative\n";
                    PC += 2;
                    break;

                case 0x10:
                    //txtDebug.Text += "BPL - Branch on Plus | Implicit \n";
                    PC += 2;
                    break;

                case 0x18:
                    //txtDebug.Text += "CLC - Clear Carry | Implicit \n";
                    C = false;
                    PC += 1;
                    break;

                case 0x20:
                    //txtDebug.Text += "JSR - Jump to Subroutine | Absolute\n";
                    PC += 3;
                    break;

                case 0x21:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Indirect with X Offset\n";
                    PC += 2;
                    break;


                case 0x25:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Zero Page\n";
                    PC += 2;
                    break;

                case 0x28:
                    //txtDebug.Text += "PLP - Pull Processor status (from stack?) | Implicit \n";
                    PC += 1;
                    break;

                case 0x29:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Immediate \n";
                    PC += 2;
                    break;

                case 0x2D:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Absolute \n";
                    PC += 3;
                    break;

                case 0x30:
                    //txtDebug.Text += "BMI - Branch on Minus | Implicit \n";
                    PC += 2;
                    break;

                case 0x31:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Indirect with Y Offset\n";
                    PC += 2;
                    break;

                case 0x38:
                    //txtDebug.Text += "SEC - Set Carry | Implicit \n";
                    C = true;
                    PC += 1;
                    break;

                case 0x35:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Zero Page with X offset\n";
                    PC += 2;
                    break;

                case 0x39:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Absolute with Y Offset\n";
                    PC += 3;
                    break;

                case 0x3D:
                    //txtDebug.Text += "AND - Bitwise AND with ACC | Absolute with X Offset\n";
                    PC += 3;
                    break;

                case 0x40:
                    //txtDebug.Text += "RTI - Return from Interrupt | Implicit \n";
                    PC += 1;
                    break;

                case 0x41:
                    //txtDebug.Text += "EOR - Exclusive OR | Indirect with X offset\n";
                    PC += 2;
                    break;

                case 0x45:
                    //txtDebug.Text += "EOR - Exclusive OR | Zero Page\n";
                    PC += 2;
                    break;

                case 0x46:
                    //txtDebug.Text += "LSR - Logical Shift Right | Zero Page \n";
                    PC += 2;
                    break;

                case 0x48:
                    //txtDebug.Text += "PHA - Push Accumulator (from Stack?) | Implicit \n";
                    PC += 1;
                    break;

                case 0x49:
                    //txtDebug.Text += "EOR - Exclusive OR | Immediate \n";
                    PC += 2;
                    break;

                case 0x50:
                    //txtDebug.Text += "BVC - Branch on Overflow Clear | Implicit \n";
                    PC += 2;
                    break;

                case 0x4A:
                    //txtDebug.Text += "LSR - Logical Shift Right | Accumulator \n";
                    PC += 1;
                    break;

                case 0x4C:
                    //txtDebug.Text += "JMP - Jump | Absolute \n";
                    PC += 3;
                    break;

                case 0x4D:
                    //txtDebug.Text += "EOR - Exclusive OR | Absolute \n";
                    PC += 3;
                    break;

                case 0x4E:
                    //txtDebug.Text += "LSR - Logical Shift Right | Absolute \n";
                    PC += 3;
                    break;

                case 0x51:
                    //txtDebug.Text += "EOR - Exclusive OR | Indirect with Y offset \n";
                    PC += 2;
                    break;

                case 0x55:
                    //txtDebug.Text += "EOR - Exclusive OR | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0x56:
                    //txtDebug.Text += "LSR - Logical Shift Right | Zero page with X offset \n";
                    PC += 2;
                    break;

                case 0x58:
                    //txtDebug.Text += "CLI - Clear Interrupt | Implicit \n";
                    PC += 1;
                    break;

                case 0x59:
                    //txtDebug.Text += "EOR - Exclusive OR | Absolute with Y offset \n";
                    PC += 3;
                    break;

                case 0x5D:
                    //txtDebug.Text += "EOR - Exclusive OR | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0x5E:
                    //txtDebug.Text += "LSR - Logical Shift Right | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0x60:
                    //txtDebug.Text += "RTS - Return from Subroutine | Implicit \n";
                    PC += 1;
                    break;

                case 0x68:
                    //txtDebug.Text += "PLA - Pull Accumulator | Implied \n";
                    PC += 1;
                    break;

                case 0x6C:
                    //txtDebug.Text += "JMP - Jump | Indirect \n";
                    PC += 3;
                    break;

                case 0x70:
                    //txtDebug.Text += "BVS - Branch on Overflow Set | Implicit \n";
                    PC += 2;
                    break;

                case 0x78:
                    //txtDebug.Text += "SEI - Set Interrupt | Implicit \n";
                    I = true;
                    PC += 1;
                    break;

                case 0x81:
                    //txtDebug.Text += "STA - Store Accumulator | Indirect with X offset \n";
                    PC += 2;
                    break;

                case 0x84:
                    //txtDebug.Text += "STY - Store Y register | Zero Page";
                    mem[PC + 1] = Y;
                    PC += 2;
                    break;

                case 0x85:
                    //txtDebug.Text += "STA - Store Accumulator | Zero Page \n";
                    PC += 2;
                    break;

                case 0x86:
                    //txtDebug.Text += "STX - Store X Register | Zero Page";
                    mem[PC + 1] = X; //Probably works like this. I gotta get used to the variables declared.
                    PC += 2;
                    break;

                case 0x88:
                    //txtDebug.Text += "DEY - Decrement Y | Implied \n";
                    Y--;

                    // Set zero flag
                    if (Y == 0)
                        Z = true;

                    // Set Negative flag
                    if (Y >> 7 == 1)
                        N = true;

                    PC += 1;
                    break;

                case 0x8A:
                    //txtDebug.Text += "TXA - Transfer X to A | Implied \n";
                    PC += 1;
                    break;

                case 0x8C:
                    //txtDebug.Text += "STY - Store Y | Absolute \n";
                    argop = (ushort)((mem[PC + 1] << 8) + (mem[PC + 2]));
                    mem[argop] = Y;
                    PC += 3;
                    break;

                case 0x8D:
                    //txtDebug.Text += "STA - Store Accumulator | Absolute \n";
                    PC += 3;
                    break;

                case 0x8E:
                    //txtDebug.Text += "STX - Store X Register | Absolute";
                    argop = (ushort)((mem[PC + 1] << 8) + (mem[PC + 2]));
                    mem[argop] = X;
                    PC += 3;
                    break;

                case 0x90:
                    //txtDebug.Text += "BCC - Branch on Carry Clear | Implicit \n";
                    PC += 2;
                    break;

                case 0x91:
                    //txtDebug.Text += "STA - Store Accumulator | Indirect with Y offset \n";
                    PC += 2;
                    break;

                case 0x94:
                    //txtDebug.Text += "STY - Store Y | Zero Page, X \n";
                    mem[(PC + 1) + X] = Y;
                    PC += 2;
                    break;

                case 0x95:
                    //txtDebug.Text += "STA - Store Accumulator | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0x96:
                    //txtDebug.Text += "STX - Store X Register | Zero Page with Y offset";
                    mem[(PC + 1) + Y] = X;
                    PC += 2;
                    break;

                case 0x98:
                    //txtDebug.Text += "TYA - Transfer Y to A | Implied \n";
                    PC += 1;
                    break;

                case 0x99:
                    //txtDebug.Text += "STA - Store Accumulator | Absolute with Y offset \n";
                    PC += 3;
                    break;

                case 0x9A:
                    //txtDebug.Text += "TXS - Transfer X to Stack Pointer | Implied \n";
                    PC += 1;
                    break;

                case 0x9D:
                    //txtDebug.Text += "STA - Store Accumulator | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0xA0:
                    //txtDebug.Text += "LDY - Load Y | Immediate \n";
                    PC += 2;
                    break;

                case 0xA1:
                    //txtDebug.Text += "LDA - Load Accumulator | Indirect with X offset \n";
                    PC += 2;
                    break;

                case 0xA2:
                    //txtDebug.Text += "LDX - Load X | Immediate \n";
                    PC += 2;
                    break;

                case 0xA4:
                    //txtDebug.Text += "LDY - Load register Y | Zero Page \n";
                    PC += 2;
                    break;

                case 0xA5:
                    //txtDebug.Text += "LDA - Load Accumulator | Zero Page \n";
                    PC += 2;
                    break;

                case 0xA6:
                    //txtDebug.Text += "LDX - Load X Register | Zero Page \n";
                    PC += 2;
                    break;

                case 0xA8:
                    //txtDebug.Text += "TAY - Transfer A to Y | Implied \n";
                    PC += 1;
                    break;

                case 0xA9:
                    //txtDebug.Text += "LDA - Load ACC | Immediate \n";
                    PC += 2;
                    break;

                case 0xAA:
                    //txtDebug.Text += "TAX - Transfer A to X | Implied \n";
                    PC += 1;
                    break;

                case 0xAC:
                    //txtDebug.Text += "LDY - Load register Y | Absolute \n";
                    PC += 3;
                    break;

                case 0xAD:
                    //txtDebug.Text += "LDA - Load ACC | Absolute \n";
                    PC += 3;
                    break;

                case 0xAE:
                    //txtDebug.Text += "LDX - Load X | Absolute \n";
                    PC += 3;
                    break;

                case 0xB0:
                    //txtDebug.Text += "BCS - Branch on Carry Set | Implicit \n";
                    PC += 2;
                    break;

                case 0xB1:
                    //txtDebug.Text += "LDA - Load Accumulator | Indirect with Y offset \n";
                    PC += 2;
                    break;

                case 0xB4:
                    //txtDebug.Text += "LDY - Load register Y | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0xB5:
                    //txtDebug.Text += "LDA - Load Accumulator | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0xB6:
                    //txtDebug.Text += "LDX - Load X Register | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0xB8:
                    //txtDebug.Text += "CLV - Clear overflow | Implied \n";
                    V = false;
                    PC += 1;
                    break;

                case 0xB9:
                    //txtDebug.Text += "LDA - Load Accumulator | Absolute with Y offset \n";
                    PC += 3;
                    break;

                case 0xBA:
                    //txtDebug.Text += "TSX - Transfer StackPtr to X | Implicit \n";
                    PC += 1;
                    break;

                case 0xBC:
                    //txtDebug.Text += "LDY - Load register Y | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0xBD:
                    //txtDebug.Text += "LDA - Load ACC | Absolute, X \n";
                    PC += 3;
                    break;

                case 0xBE:
                    //txtDebug.Text += "LDX - Load X Register | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0xC0:
                    //txtDebug.Text += "CPY - Compare Y reg | Immediate \n";
                    PC += 2;
                    break;

                case 0xC1:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Indirect with X offset \n";
                    PC += 2;
                    break;

                case 0xC4:
                    //txtDebug.Text += "CPY - Compare Y reg | Zero Page \n";
                    PC += 2;
                    break;

                case 0xC5:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Zero Page \n";
                    PC += 2;
                    break;

                case 0xC6:
                    //txtDebug.Text += "DEC - Decrement Memory | Zero Page \n";
                    PC += 2;
                    break;

                case 0xC8:
                    //txtDebug.Text += "INY - Increment Y | Implied \n";

                    Y++;

                    // Set Zero flag
                    if (Y == 0)
                        Z = true;

                    // Set Negative flag
                    if (Y >> 7 == 1)
                        N = true;

                    PC += 1;
                    break;

                case 0xC9:
                    //txtDebug.Text += "CMP - Compare | Immediate \n";
                    PC += 2;
                    break;

                case 0xCA:
                    //txtDebug.Text += "DEX - Decrement X | Implied \n";
                    X--;
                    PC += 1;
                    break;

                case 0xCC:
                    //txtDebug.Text += "CPY - Compare Y reg | Absolute \n";
                    PC += 3;
                    break;

                case 0xCD:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Absolute \n";
                    PC += 3;
                    break;

                case 0xCE:
                    //txtDebug.Text += "DEC - Decrement Memory | Absolute \n";
                    PC += 3;
                    break;

                case 0xD0:
                    //txtDebug.Text += "BNE - Branch on Not Equal (!=) | Implicit \n";
                    PC += 2;
                    break;

                case 0xD1:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Indirect with Y offset \n";
                    PC += 2;
                    break;

                case 0xD5:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0xD6:
                    //txtDebug.Text += "DEC - Decrement Memory | Zero Page with X offset \n";
                    PC += 2;
                    break;

                case 0xD8:
                    //txtDebug.Text += "CLD - Clear decimal | Implicit \n";
                    D = false;
                    PC += 1;
                    break;

                case 0xD9:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Absolute with Y offset \n";
                    PC += 2;
                    break;

                case 0xDD:
                    //txtDebug.Text += "CMP - Compare Accumulator reg | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0xDE:
                    //txtDebug.Text += "DEC - Decrement Memory | Absolute with X offset \n";
                    PC += 3;
                    break;

                case 0xE0:
                    //txtDebug.Text += "CPX - Compare X reg | Immediate \n";
                    PC += 2;
                    break;

                case 0xE4:
                    //txtDebug.Text += "CPX - Compare X reg | Zero Page \n";
                    PC += 2;
                    break;

                case 0xE6:
                    //txtDebug.Text += "INC - Incremental mem | Zero Page \n";
                    PC += 2;
                    break;

                case 0xE8:
                    //txtDebug.Text += "INX - Increment X | Implied \n";
                    X++;

                    // Set Zero flag
                    if (X == 0)
                        Z = true;

                    // Set Negative flag
                    if (X >> 7 == 1)
                        N = true;

                    PC += 1;
                    break;

                case 0xEA:
                    //txtDebug.Text += "NOP - No Operation (sleep) \n";
                    PC += 1;
                    break;

                case 0xEC:
                    //txtDebug.Text += "CPX - Compare X Register | Absolute \n";
                    PC += 3;
                    break;

                case 0xEE:
                    //txtDebug.Text += "INC - Increment Memory | Absolute \n";
                    PC += 3;
                    break;

                case 0xF0:
                    //txtDebug.Text += "BEQ - Branch on Equal(==) | Implicit \n";
                    PC += 2;
                    break;

                case 0xF6:
                    //txtDebug.Text += "INC - Incremental mem | Zero Page with X offset\n";
                    PC += 2;
                    break;

                case 0xF8:
                    //txtDebug.Text += "SED - Set decimal | Implied \n";
                    D = true;
                    PC += 1;
                    break;

                case 0xFE:
                    //txtDebug.Text += "INC - Incremental mem | Absolute with X offset \n";
                    PC += 3;
                    break;

                default:
                    //txtDebug.Text += "Undefined";
                    break;
            }
        }

        public void InitVM()
        {
            // Clear registers
            A = 0x00;
            X = 0x00;
            Y = 0x00;
            P = 0x34;
            PC = 0x08000;

            // Clear flags
            N = V = B = D = I = Z = C = false;

            // Clear RAM
            for (int i = 0x0000; i < 0x07FF; i++)
            {
                mem[i] = 0xFF;
            }
        }
    }
}
