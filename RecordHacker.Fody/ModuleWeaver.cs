using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace RecordHacker.Fody
{
public class ModuleWeaver :
    BaseModuleWeaver
{

    public override void Execute()
    {
        var ex = ModuleDefinition.Types.SingleOrDefault(t => t.Name == "RecordException");

        var m = ex.GetMethods().Single(mm => mm.Name == "RecordClone");
        m.Name = "<Clone>$";

        // var testm = ex.GetMethods().Single(mm => mm.Name == "TestMethod_Bazmek");
        // testm.Name = "TestMethod";
    }

    public override IEnumerable<string> GetAssembliesForScanning()
    {
        yield return "netstandard";
        yield return "mscorlib";
    }

    public override bool ShouldCleanReference => true;
}
}
