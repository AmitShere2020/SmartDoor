using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using DoubleR_ES.Models;

namespace DoubleR_ES.Json_Model
{
    public class Json
    {
        public ProfileType ProfileType { get; set; }

        public double GaugeSize { get; set; }

        public ProfileInfo ProfileInfo { get; set; }

        public BendCollection HingeDataList { get; set; }

        public Tab TabData { get; set; }

        public BendCollection TopViewDataList { get; set; }

        public TopViewData TopViewData { get; set; }

        protected List<Entity> Entities { get; private set; }
        protected List<Line> ProfileLines { get; private set; }

        public InputData inputData;

        public Json(ProfileType profileType)
        {
            ProfileType = profileType;
        }

        public virtual void CollectBendData()
        {

        }

        protected virtual void GetTopBendData()
        {

        }

        protected virtual void GetHingeBendData()
        {

        }

        private void ReadFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                ReadAutodesk readAutodesk = new ReadAutodesk(fileName);
                readAutodesk.DoWork();
                Entities = readAutodesk.Entities.ToList();
            }
        }

        // Read each line in sequence but first line is >30;
        private List<Line> ReadDxfInSeq()
        {
            var listOfLines = Entities.OfType<Line>().ToList();

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

        private List<Line> GetContinuity(Line currentLine, List<Line> lines)
        {
            var listOfLines = new List<Line> { currentLine };

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

        protected List<Line> JoinCoLinearLines(List<Line> lines)
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

        public void FetchDxf(string fileName)
        {
            ReadFile(fileName); // Collect all entities here

            var allSeqLines = ReadDxfInSeq(); // Read all line in sequence
            var profLines = allSeqLines.Where(line => line.LayerName == "PROFILE").ToList();
            var tabLines = allSeqLines.Where(line => line.LayerName == "TABS").ToList();

            if (profLines.Count == 0)
            {
                MessageBox.Show("Profile line are not availabel.Check std", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Read data from hinge side
            if (tabLines.Count > 0)
            {
                TabData = new Tab
                {
                    Count = tabLines.Count / 3,
                    TabBase = inputData.TabBase,
                    TabTop = inputData.TabTop
                };
            }
            ProfileLines = JoinCoLinearLines(profLines);

            //HingeDataList.AddRange(allowances);



            //// Read data from Top side
            //else

            //{
            //    var profileLines = profLines;

            //}
        }
    }
}
