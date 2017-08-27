using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public static class DateHelpers
    {
        public static string GetAge(this DateTime date)
        {
            return GetAge(date, DateTime.Now);
        }

        public static string GetAge(this DateTime Bday, DateTime Cday)
        {
            int Years, Months, Days;
            if ((Cday.Year - Bday.Year) > 0 ||
                    (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                    ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                if (Cday.Month > Bday.Month)
                {
                    Years = Cday.Year - Bday.Year;
                    Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == Bday.Month)
                {
                    if (Cday.Day >= Bday.Day)
                    {
                        Years = Cday.Year - Bday.Year;
                        Months = 0;
                        Days = Cday.Day - Bday.Day;
                    }
                    else
                    {
                        Years = (Cday.Year - 1) - Bday.Year;
                        Months = 11;
                        Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                    }
                }
                else
                {
                    Years = (Cday.Year - 1) - Bday.Year;
                    Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }

                return GetStringAge(Years, Months, Days);
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }
        }

        public static string GetStringAge(int Years, int Months, int Days)
        {
            string ageString = string.Empty;
            if (Years == 1 && Months == 1)
            {
                ageString = string.Format("1 año 1 mes");
            }
            else if (Years == 1)
            {
                ageString = string.Format("1 año {0} meses", Months);
            }
            else if (Years < 1 && Months == 1 && Days == 1)
            {
                ageString = string.Format("1 mes 1 día");
            }
            else if (Years < 1 && Months == 1)
            {
                ageString = string.Format("1 mes {0} días", Days);
            }
            else if (Years < 1 && Months < 1 && Days == 1)
            {
                ageString = string.Format("{0} día", Days);
            }
            else if (Years < 1 && Months < 1)
            {
                ageString = string.Format("{0} días", Days);
            }
            else if (Years < 1 && Days == 1)
            {
                ageString = string.Format("{0} meses 1 día", Years * 12 + Months);
            }
            else if (Years < 1)
            {
                ageString = string.Format("{0} meses {1} días", Years * 12 + Months, Days);
            }
            else if (Years >= 2 && Months == 1)
            {
                ageString = string.Format("{0} años 1 mes", Years, Months);
            }
            else // if (Years >= 2 && Months != 1)
            {
                ageString = string.Format("{0} años {1} meses", Years, Months);
            }
            return ageString;
        }
    }
}
