using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FastLineChart
{
    public class CustomTrackBallBehavior : ChartTrackBallBehavior

    {

        public DataTemplate CustomLabelTemplate

        {

            get { return (DataTemplate)GetValue(CustomLabelTemplateProperty); }

            set { SetValue(CustomLabelTemplateProperty, value); }

        }



        // Using a DependencyProperty as the backing store for CustomLabelTemplate.  This enables animation, styling, binding, etc.

        public static readonly DependencyProperty CustomLabelTemplateProperty =

            DependencyProperty.Register("CustomLabelTemplate", typeof(DataTemplate), typeof(CustomTrackBallBehavior), new PropertyMetadata(null));



        protected override void GenerateLabels()

        {

            try
            {
                AddLabel(PointInfos[0], LabelVerticalAlignment, LabelHorizontalAlignment, PointInfos[0].Series.TrackBallLabelTemplate);

                //Custom label.

                AddLabel(PointInfos[0], LabelVerticalAlignment, LabelHorizontalAlignment, CustomLabelTemplate);

            }
            catch 
            {

                
            }
        }

    }



    public class CustomLabel

    {

        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }
}
