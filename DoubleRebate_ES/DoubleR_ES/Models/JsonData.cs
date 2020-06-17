using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using devDept.Graphics;
using devDept.Serialization;

namespace DoubleR_ES.Models
{
    public class JsonData
    {
        public double GaugeSize { get; set; }
        public ProfileInfo ProfileInfo { get; set; }
        public BendCollection HingeDataList { get; set; }
        public Tab Tab { get; set; }
        public BendCollection TopViewDataList { get; set; }
        public TopViewData TopViewData { get; set; }
        public ProfileType ProfileType { get; set; }

        private List<Entity> entities;

        private readonly InputData inputData;

        public JsonData(ProfileType profileType)
        {
            ProfileType = profileType;
            inputData = Utilities.InputData;
            HingeDataList = new BendCollection();
            TopViewDataList = new BendCollection();
        }

        private void ReadFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                ReadAutodesk readAutodesk = new ReadAutodesk(fileName);
                readAutodesk.DoWork();
                entities = readAutodesk.Entities.ToList();
            }
        }

        public void FetchDXF(string fileName)
        {
            ReadFile(fileName);
            var allSeqLines = ReadDxfInSeq(); // Read all line in sequence
            var profLines = allSeqLines.Where(line => line.LayerName == "PROFILE").ToList();
            var tabLines = allSeqLines.Where(line => line.LayerName == "TABS").ToList();

            if (profLines.Count == 0)
            {
                MessageBox.Show("Profile line are not availabel.Check std", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (tabLines.Count > 0) // hinge
            {
                /********** Hinge View Points*********/
                Tab = new Tab
                {
                    Count = tabLines.Count / 3,
                    TabBase = inputData.TabBase,
                    TabTop = inputData.TabTop
                };

                var profileLines = JoinColinearLines(profLines);
                SetLineTypes();

                double angleFactor = Math.Cos((Math.PI * 45) / 180);

                List<BendData> allowances = new List<BendData>()
                {
                    new BendData()
                    {
                        Line = profileLines[0],
                        BendAllowance = inputData.Return1 - profileLines[0].Length(),
                        LineType = LineType.Return_1
                    },
                    new BendData()
                    {
                        Line = profileLines[1],
                        BendAllowance = inputData.Architrave1 - profileLines[1].Length() * angleFactor,
                        LineType = LineType.Architrave_1
                    },
                    new BendData()
                    {
                        Line = profileLines[2],
                        BendAllowance = inputData.Rebate1 - profileLines[2].Length(),
                        LineType = LineType.Rebate_1
                    },
                    new BendData()
                    {
                        Line = profileLines[3],
                        BendAllowance = inputData.StopHgt1 - profileLines[3].Length(),
                        LineType = LineType.StopHgt_1
                    },
                    new BendData()
                    {
                        Line = profileLines[4],
                        BendAllowance = inputData.Throat - profileLines[4].Length(),
                        LineType = LineType.Throat
                    },
                    new BendData()
                    {
                        Line = profileLines[5],
                        BendAllowance = inputData.StopHgt2 - profileLines[5].Length(),
                        LineType = LineType.StopHgt_2
                    },
                    new BendData()
                    {
                        Line = profileLines[6],
                        BendAllowance = inputData.Rebate2 - profileLines[6].Length(),
                        LineType = LineType.Rebate_2
                    },
                    new BendData()
                    {
                        Line = profileLines[7],
                        BendAllowance = inputData.Architrave2 - profileLines[7].Length() * angleFactor,
                        LineType = LineType.Architrave_2
                    },
                    new BendData()
                    {
                        Line = profileLines[8],
                        BendAllowance = inputData.Return2 - profileLines[8].Length(),
                        LineType = LineType.Return_2
                    },
                };

                HingeDataList.AddRange(allowances);
                double depth = inputData.Return1 + inputData.Architrave1 + inputData.Rebate1 + inputData.Throat +
                               inputData.Rebate2 + inputData.Architrave2 + inputData.Return2;

                ProfileInfo = new ProfileInfo
                {
                    Return1 = inputData.Return1,
                    Architrave1 = inputData.Architrave1,
                    Rebate1 = inputData.Rebate1,
                    StopHgt1 = inputData.StopHgt1,
                    StopSection = inputData.StopSection,
                    StopHgt2 = inputData.StopHgt2,
                    Rebate2 = inputData.Rebate2,
                    Architrave2 = inputData.Architrave2,
                    Return2 = inputData.Return2,
                    Throat = inputData.Throat,
                    RevealHeight = inputData.RevealHeight,
                    RevealWidth = inputData.RevealWidth,
                    Depth = depth,
                };
            }
            else
            {
                /********** Top View Points*********/
                var profileLines = profLines;
                double angleFactor = Math.Cos((Math.PI * 45) / 180);

                BendCollection allowances = new BendCollection
                {
                    new BendData()
                    {
                        Line = profileLines[0],
                        BendAllowance = inputData.Return1 - profileLines[0].Length(),
                        LineType = LineType.Return_1
                    },
                    new BendData()
                    {
                        Line = profileLines[1],
                        BendAllowance = inputData.Architrave1 - profileLines[1].Length() * angleFactor,
                        LineType = LineType.Architrave_1
                    },
                    new BendData()
                    {
                        Line = profileLines[2],
                        BendAllowance = 0,
                        LineType = LineType.Rebate_1
                    },
                    new BendData()
                    {
                        Line = profileLines[3],
                        BendAllowance = 0,
                        LineType = LineType.StopHgt_1
                    },

                    new BendData()
                    {
                        Line = profileLines[4],
                        BendAllowance = 0, // this is not actual bend allowance
                        // this is length of so called throat
                        LineType = LineType.Throat // its length is depend on other parameters
                    },

                    new BendData()
                    {
                        Line = profileLines[5],
                        BendAllowance = 0,
                        LineType = LineType.StopHgt_2
                    },
                    new BendData()
                    {
                        Line = profileLines[6],
                        BendAllowance = 0,
                        LineType = LineType.Rebate_2
                    },
                    new BendData()
                    {
                        Line = profileLines[7],
                        BendAllowance = inputData.Architrave2 - profileLines[7].Length() * angleFactor,
                        LineType = LineType.Architrave_2
                    },
                    new BendData()
                    {
                        Line = profileLines[8],
                        BendAllowance = inputData.Return2 - profileLines[8].Length(),
                        LineType = LineType.Return_2
                    },
                };

                //var throatData = new BendData()
                //{
                //    Line = profileLines[4],
                //    BendAllowance = allowances[LineType.Rebate_2].Line.StartPoint.Y -   // this is not actual bend allowance
                //                    allowances[LineType.Rebate_1].Line.EndPoint.Y,      // this is length of so called throat
                //    LineType = LineType.Throat                                          // its length is depend on other parameters
                //};

                allowances[LineType.Throat].BendAllowance = allowances[LineType.Rebate_2].Line.StartPoint.Y -
                                                            allowances[LineType.Rebate_1].Line.EndPoint.Y;


                TopViewDataList.AddRange(allowances); //Utilities.GetVertices(profLines);


                TopViewData = new TopViewData
                {
                    ConstVLine3 = profLines[2].Length(),
                    ConstHLine4 = profLines[3].Length(),
                    MidWidth = profLines[4].Length(),
                };
            }
        }

        private void SetLineTypes()
        {
            switch (ProfileType)
            {
                case ProfileType.MDF:
                    break;
                case ProfileType.ADL:
                    break;
            }
        }

        private List<Line> JoinColinearLines(List<Line> lines)
        {
            var modifiedList = new List<Line>();
            Point3D startPoint = null;
            Point3D endPoint = null;
            bool startJoining = false;
            for (int i = 0; i < lines.Count; i++)
            {
                var firstLine = lines[i];
                if (i + 1 == lines.Count)
                {
                    modifiedList.Add(firstLine);
                    break;
                }

                var secondLine = lines[i + 1];

                var firstAngle = Utility.RadToDeg(firstLine.Direction.AngleInXY);
                var secondAngle = Utility.RadToDeg(secondLine.Direction.AngleInXY);
                var abs = Math.Abs(firstAngle - secondAngle);
                if (abs == 0 || abs == 180)
                {
                    if (!startJoining)
                    {
                        startPoint = firstLine.StartPoint;
                        endPoint = secondLine.EndPoint;
                        startJoining = true;
                    }
                    else
                    {
                        endPoint = secondLine.EndPoint;
                    }
                }
                else
                {
                    if (startJoining)
                    {
                        Line combinedLine = new Line(startPoint, endPoint);
                        modifiedList.Add(combinedLine);
                        startJoining = false;
                    }
                    else
                    {
                        modifiedList.Add(firstLine);
                    }
                }
            }

            return modifiedList;
        }

        private List<Line> GetContinuity(Line currentLine, List<Line> lines)
        {
            var listOfLines = new List<Line> {currentLine};

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line == currentLine)
                {
                    lines.Remove(line);
                    break;
                }
            }

            while (lines.Count > 0)
            {
                var nearbyLine = new List<Line>();
                var baseForwardPoint = currentLine.EndPoint;
                var basebackwardPoint = currentLine.StartPoint;

                foreach (var line in lines)
                    if (line.StartPoint.Equals(baseForwardPoint) || line.EndPoint.Equals(baseForwardPoint) ||
                        line.StartPoint.Equals(basebackwardPoint) || line.EndPoint.Equals(basebackwardPoint))
                        if (!nearbyLine.Contains(line))
                            nearbyLine.Add(line);

                currentLine = nearbyLine[0];

                if (!listOfLines.Contains(currentLine))
                {
                    listOfLines.Add(currentLine);
                    for (var i = 0; i < lines.Count; i++)
                    {
                        var line = lines[i];
                        if (line == currentLine)
                        {
                            lines.Remove(line);
                            break;
                        }
                    }
                }
            }

            return listOfLines;
        }

        // Read each line in sequence but first line is >30;
        private List<Line> ReadDxfInSeq()
        {
            var listOfLines = entities.OfType<Line>().ToList();

            if (listOfLines.Count == 0)
            {
                MessageBox.Show("Profile line are not availabel.Check std", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }

            var firstLine = listOfLines.First(line => line.StartPoint.Equals(Point3D.Origin));
            var points = Utilities.GetVertices(listOfLines);
            var clockwise = Utility.IsOrientedClockwise(points); // IsClockwisePolygon(points);
            ICurve curve = UtilityEx.SmartAdd(listOfLines.OfType<ICurve>().ToList(), true);

            if (!clockwise)
                listOfLines.Reverse();

            // 0,0 clockwise
            var allSeqLines = GetContinuity(firstLine, listOfLines); // in clockwise manner
            var lastLine = allSeqLines[0];
            if (lastLine.Length() > 30) // to start from 0,0.
            {
                allSeqLines.RemoveAt(0);
                allSeqLines.Add(lastLine);
            }

            OrderEntityList(allSeqLines);

            return allSeqLines;
        }

        private void OrderEntityList(List<Line> unorderedEntities)
        {
            var basePoint = new Point3D(0, 0);
            foreach (var line in unorderedEntities)
            {
                if (line.EndPoint.Equals(basePoint))
                {
                    line.Reverse();
                    basePoint = line.EndPoint;
                }
                else
                {
                    basePoint = line.StartPoint;
                }
            }
        }
    }
}