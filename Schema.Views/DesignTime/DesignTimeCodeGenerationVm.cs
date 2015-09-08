using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;
using Schema.Common.Interfaces;

namespace Schema.Views.DesignTime
{
    class DesignTimeCodeGenerationVm:ICodeGenerationVm
    {
        public ObservableCollection<Template> Templates { get; set; } = new ObservableCollection<Template>();
        public string GeneratedText { get; set; }
        public SchemaObject SchemaObject { get; set; }
        public ICommand GenerateCodeCommand { get; }
    }
}
