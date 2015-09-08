using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Schema.Common.DataTypes;
using Schema.Common.DataTypes.CodeGeneration;
using Schema.Common.Interfaces;
using Schema.ViewModels.Annotations;

namespace Schema.ViewModels
{
   public class CodeGenerationVm:ICodeGenerationVm, INotifyPropertyChanged
    {
       private SchemaObject schemaObject;

       public ObservableCollection<Template> Templates { get; set; }
       public string GeneratedText { get; set; }

       public SchemaObject SchemaObject
       {
           get { return schemaObject; }
           set
           {
               schemaObject = value;
               OnPropertyChanged();
           }
       }

       public ICommand GenerateCodeCommand { get; }
       public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       }
    }
}
