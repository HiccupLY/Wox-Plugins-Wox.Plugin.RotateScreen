using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDisplayAPI;
using Wox.Plugin;

namespace ScreenRotation
{
    public class Main :IPlugin
    {
        
        public Dictionary<string, MyDisplay.Orientations> RotationDict { get; set; }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            Device device = null;
            int deviceIndex = 0;
            if (query.Search == String.Empty)
            {
                return HelloWorld(results);
            }
            else
            {
                try
                {
                    deviceIndex = Convert.ToInt32(query.FirstSearch);
                }
                catch 
                {
                    deviceIndex = 0;
                }

                if (deviceIndex != 0)
                {
                    device = MyDisplay.GetDisplay(Convert.ToUInt32(deviceIndex));
                    if (device == null)
                    {
                        Result rotateResult = new Result() {Title = "No device?!",};
                        results.Add(rotateResult);
                    }
                    else
                    {
                        results.Clear();
                        if (query.SecondSearch == string.Empty)
                        {
                            foreach (var direction in RotationDict.Keys)
                            {
                                Result rotateResult = new Result()
                                {
                                    Title = device.ToString(),
                                    IcoPath = "Images\\rotate.png",
                                    SubTitle = String.Format("Rotated direction: ClockWise {0}", direction),
                                    Action = (c) =>
                                        MyDisplay.Rotate(Convert.ToUInt32(deviceIndex), RotationDict[direction])
                                };
                                results.Add(rotateResult);
                            }
                        }
                        else
                        {
                            var selectedDirection = RotationDict.Keys
                                .Where(e => e.Contains(query.SecondSearch.ToUpper())).ToList();
                            if (selectedDirection.Count() != 0)
                            {
                                foreach (var direction in selectedDirection)
                                {
                                    Result rotateResult = new Result()
                                    {
                                        Title = device.ToString(),
                                        IcoPath = "Images\\rotate.png",
                                        SubTitle = String.Format("Rotated direction: ClockWise {0}", direction),
                                        Action = (c) => MyDisplay.Rotate(Convert.ToUInt32(deviceIndex),
                                            RotationDict[direction])
                                    };
                                    results.Add(rotateResult);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return SetInitial(results);
                }
            }

            return results;
        }

        private static List<Result> HelloWorld(List<Result> results)
        {
            results.Clear();
            results.Add(new Result()
            {
                Title = "Hello!",
                IcoPath = "Images\\app.png",
                SubTitle = "First add the selected monitor index, then select the direction",
                Action = (c) =>
                {
                    //MyDisplay.ResetAllRotations();
                    return true;
                }
            });
            return results;
        }

        private static List<Result> SetInitial(List<Result> results)
        {
            results.Clear();
            results.Add(new Result()
            {
                Title = "All Devices",
                IcoPath = "Images\\rotate.png",
                SubTitle = "Rotate to initial orientation",
                Action = (c) =>
                {
                    MyDisplay.ResetAllRotations();
                    return true;
                }
            });
            return results;
        }

        public void Init(PluginInitContext context)
        {
            RotationDict = new Dictionary<string, MyDisplay.Orientations>()
            {
                {"UP", MyDisplay.Orientations.DEGREES_CW_0},
                {"LEFT", MyDisplay.Orientations.DEGREES_CW_270},
                {"DOWN", MyDisplay.Orientations.DEGREES_CW_180},
                {"RIGHT", MyDisplay.Orientations.DEGREES_CW_90}
            };

            //throw new NotImplementedException();
        }
    }
}
