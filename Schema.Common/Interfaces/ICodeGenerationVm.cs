using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;

namespace Schema.Common.Interfaces
{
 public   interface ICodeGenerationVm
    {
        ObservableCollection<Template> Templates { get; set; } 
        string GeneratedText { get; set; }

        SchemaObject SchemaObject { get; set; }

        ICommand GenerateCodeCommand { get;   }
    }
}
