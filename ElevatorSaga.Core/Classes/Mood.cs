using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSaga.Core.Classes
{
    public enum Mood
    {
        Happy,
        Normal,
        Angry
    }

    public class MoodHelper
    {
        public static Mood GetMoodBySla( int sl )
        {
            Mood retVal = Mood.Happy;
            if (sl < 67) retVal = Mood.Normal;
            else if (sl < 33) retVal = Mood.Angry;

            return retVal;
        }
    }
}
