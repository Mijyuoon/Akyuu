using Akyuu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// !! WORK IN PROGRESS !!
namespace Akyuu.Filter {
    public class ScreenshotFilter {
        private static FilterActionData[] ActionDefs = new[] {
            FilterActionData.Define<FilenameFilterAction>("\\s*\"([^\"]*)\"\\s*"),
            FilterActionData.Define<ModifiedAfterFilterAction>(@"\s*>\(([^)]*)\)\s*"),
            FilterActionData.Define<ModifiedBeforeFilterAction>(@"\s*<\(([^)]*)\)\s*"),
        };

        private List<IFilterAction> actions;

        public ScreenshotFilter(string format) {
            actions = new List<IFilterAction>();

            int matchOffset = 0;
            while(matchOffset < format.Length) {
                foreach(var adef in ActionDefs) {
                    var m = adef.Regex.Match(format, matchOffset);

                    if(m.Success && m.Index == matchOffset) {
                        actions.Add(adef.Create(m.Groups[1].Value));

                        matchOffset += m.Length;
                        goto continueOuter;
                    }
                }

                throw new Exception("Failed to parse filter string");
                continueOuter:;
            }
        }

        public void Apply(ref IEnumerable<Screenshot> screenshots) {
            foreach(var act in actions) {
                act.Apply(ref screenshots);
            }
        }
    }
}
