using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using DoubleR_ES.Models;

namespace DoubleR_ES.Json_Model
{
    public class MDFJson:Json
    {
        
        public MDFJson(ProfileType profileType):base(profileType)
        {
            inputData = Utilities.InputData;
            HingeDataList = new BendCollection();
            TopViewDataList = new BendCollection();
        }

        public override void CollectBendData()
        {
            if (TabData.Count>0)
            {
                GetHingeBendData();
            }
            else
            {
                GetTopBendData();
            }
        }

        protected override void GetTopBendData()
        {
            double angleFactor = Math.Cos((Math.PI * 45) / 180);

            BendCollection allowances = new BendCollection
                {
                    new BendData()
                    {
                        Line = ProfileLines[0],
                        BendAllowance = inputData.Return1 - ProfileLines[0].Length(),
                        LineType = LineType.Return_1
                    },
                    new BendData()
                    {
                        Line = ProfileLines[1],
                        BendAllowance = inputData.Architrave1 - ProfileLines[1].Length() * angleFactor,
                        LineType = LineType.Architrave_1
                    },
                    new BendData()
                    {
                        Line = ProfileLines[2],
                        BendAllowance = 0,
                        LineType = LineType.Rebate_1
                    },
                    new BendData()
                    {
                        Line = ProfileLines[3],
                        BendAllowance = 0,
                        LineType = LineType.StopHgt_1
                    },

                    new BendData()
                    {
                        Line = ProfileLines[4],
                        BendAllowance = 0, // this is not actual bend allowance
                        // this is length of so called throat
                        LineType = LineType.Throat // its length is depend on other parameters
                    },

                    new BendData()
                    {
                        Line = ProfileLines[5],
                        BendAllowance = 0,
                        LineType = LineType.StopHgt_2
                    },
                    new BendData()
                    {
                        Line = ProfileLines[6],
                        BendAllowance = 0,
                        LineType = LineType.Rebate_2
                    },
                    new BendData()
                    {
                        Line = ProfileLines[7],
                        BendAllowance = inputData.Architrave2 - ProfileLines[7].Length() * angleFactor,
                        LineType = LineType.Architrave_2
                    },
                    new BendData()
                    {
                        Line = ProfileLines[8],
                        BendAllowance = inputData.Return2 - ProfileLines[8].Length(),
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
                ConstVLine3 = ProfileLines[2].Length(),
                ConstHLine4 = ProfileLines[3].Length(),
                MidWidth = ProfileLines[4].Length(),
            };
        }

        protected override void GetHingeBendData()
        {
            double angleFactor = Math.Cos((Math.PI * 45) / 180); // consider angle is always 45 deg fro architrave

            List<BendData> allowances = new List<BendData>()
            {
                new BendData()
                {
                    Line = ProfileLines[0],
                    BendAllowance = inputData.Return1 - ProfileLines[0].Length(),
                    LineType = LineType.Return_1
                },
                new BendData()
                {
                    Line = ProfileLines[1],
                    BendAllowance = inputData.Architrave1 - ProfileLines[1].Length() * angleFactor,
                    LineType = LineType.Architrave_1
                },
                new BendData()
                {
                    Line = ProfileLines[2],
                    BendAllowance = inputData.Rebate1 - ProfileLines[2].Length(),
                    LineType = LineType.Rebate_1
                },
                new BendData()
                {
                    Line = ProfileLines[3],
                    BendAllowance = inputData.StopHgt1 - ProfileLines[3].Length(),
                    LineType = LineType.StopHgt_1
                },
                new BendData()
                {
                    Line = ProfileLines[4],
                    BendAllowance = inputData.Throat - ProfileLines[4].Length(),
                    LineType = LineType.Throat
                },
                new BendData()
                {
                    Line = ProfileLines[5],
                    BendAllowance = inputData.StopHgt2 - ProfileLines[5].Length(),
                    LineType = LineType.StopHgt_2
                },
                new BendData()
                {
                    Line = ProfileLines[6],
                    BendAllowance = inputData.Rebate2 - ProfileLines[6].Length(),
                    LineType = LineType.Rebate_2
                },
                new BendData()
                {
                    Line = ProfileLines[7],
                    BendAllowance = inputData.Architrave2 - ProfileLines[7].Length() * angleFactor,
                    LineType = LineType.Architrave_2
                },
                new BendData()
                {
                    Line = ProfileLines[8],
                    BendAllowance = inputData.Return2 - ProfileLines[8].Length(),
                    LineType = LineType.Return_2
                }

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
    }
}