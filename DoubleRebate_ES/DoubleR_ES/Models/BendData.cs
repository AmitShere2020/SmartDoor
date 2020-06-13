using devDept.Eyeshot.Entities;

namespace DoubleR_ES.Models
{
    public class BendData
    {
        public Line Line { get; set; }
        public double BendAllowance { get; set; }
        public double ModifiedLengthTxt { get; set; } // taken from form
        public LineType LineType { get; set; }
        public double Displacement { get; set; }

    }
}