
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.33440
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly App
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module App.exe
// MVID: {608B7E23-EE91-4F5A-9CD1-C4189B1E8A10}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x00AB0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit App
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       27 (0x1b)
    .maxstack  1
    .locals init (int32 V_0,
             class [mscorlib]System.Type V_1)
    IL_0000:  nop
    IL_0001:  ldc.i4     0x254
    IL_0006:  stloc.0
    IL_0007:  ldloc.0
    IL_0008:  box        [mscorlib]System.Int32
    IL_000d:  callvirt    instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
    IL_0012:  stloc.1
    IL_0013:  ldloc.1
    IL_0014:  call       void [mscorlib]System.Console::WriteLine(object)
    IL_0019:  nop
    IL_001a:  ret
  } // end of method App::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method App::.ctor

} // end of class App


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file C:\MyFolder\ISEL\pg5 ave - 2016-2017 - 1º sem\aulas\aula19-gettype-valuetypes\App.res
