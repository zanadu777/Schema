using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using Schema.Common.DataTypes;

namespace Schema.Common.Visualizers
{
    public class DbTableVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService == null)
                throw new ArgumentNullException("windowService");
            if (objectProvider == null)
                throw new ArgumentNullException("objectProvider");

         
            var data = (DbTable)objectProvider.GetObject();
            using (var displayForm = new DbTableVisualizerForm())
            {
                displayForm.Table = data;
                displayForm.Text = data.ToString();
                windowService.ShowDialog(displayForm);
            }
        }

        // TODO: Add the following to your testing code to test the visualizer:
        // 
        //    DbTableVisualizer.TestShowVisualizer(new SomeType());
        // 
        /// <summary>
        /// Tests the visualizer by hosting it outside of the debugger.
        /// </summary>
        /// <param name="objectToVisualize">The object to display in the visualizer.</param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DbTableVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
