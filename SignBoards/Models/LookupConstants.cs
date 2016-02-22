using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignBoards.Models
{
    public class LookupConstants
    {
        public class Signboard
        {
            public class Fitting
            {
                public class Type
                {
                    private static readonly string LifeTimeGuidStriing = "52E0763A-5AC7-462C-BE67-AFE6BEEF93B5";
                    private static readonly string SingleGuidStriing = "704A8D61-E39C-4FE1-AA45-FACD62E09279";

                    public static string SingleText = "Single";
                    public static string LifeTimeText = "Life Time";

                    public static Guid LifeTime = Guid.Parse(LifeTimeGuidStriing);
                    public static Guid Single = Guid.Parse(SingleGuidStriing);
                }
            }
        }
    }
}