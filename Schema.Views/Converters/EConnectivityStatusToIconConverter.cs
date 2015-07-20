//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Schema.Common.DataTypes;

//namespace Schema.Views.Converters
//{
//    public class EConnectivityStatusToIconConverter
//    {
//        public object Convert(object value, Type targetType,
//       object parameter, CultureInfo culture)
//        {
//            var status = (EConnectivityStatus)value;
//            switch (status)
//            {
//                case EConnectivityStatus.Connected:
//                    break;
//                case EConnectivityStatus.Disconnected:
//                    break;
//                case EConnectivityStatus.Unknown:
//                    break;
//                case EConnectivityStatus.Testing:
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//        }

//        public object ConvertBack(object value, Type targetType,
//            object parameter, CultureInfo culture)
//        {
//            throw new Exception("Not Implemented");
//        }

//    }
//}
