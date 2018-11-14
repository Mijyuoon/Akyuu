using Akyuu.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// !! WORK IN PROGRESS !!
namespace Akyuu.Filter {
    interface IFilterAction {
        void Configure(string data);
        void Apply(ref IEnumerable<Screenshot> screenshots);
    }

    class FilterActionData {
        public Type Action { get; }
        public Regex Regex { get; }

        private FilterActionData(Type action, string regex) {
            this.Action = action;
            this.Regex = new Regex(regex, RegexOptions.Compiled);
        }

        public IFilterAction Create(string data) {
            var action = (IFilterAction)Activator.CreateInstance(Action);
            action.Configure(data);
            return action;
        }

        public static FilterActionData Define<T>(string regex) where T : IFilterAction, new() {
            return new FilterActionData(typeof(T), regex);
        }
    }

    class FilenameFilterAction : IFilterAction {
        private string matcher;

        public void Configure(string data) {
            matcher = data;
        }

        public void Apply(ref IEnumerable<Screenshot> screenshots) {
            screenshots = from t in screenshots
                          where t.File.Contains(matcher)
                          select t;
        }
    }

    class ModifiedAfterFilterAction : IFilterAction {
        private DateTime matcher;

        public void Configure(string data) {
            matcher = DateTime.Parse(data, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
        }

        public void Apply(ref IEnumerable<Screenshot> screenshots) {
            screenshots = from t in screenshots
                          where File.GetLastWriteTime(t.Source.AbsolutePath) > matcher
                          select t;
        }
    }

    class ModifiedBeforeFilterAction : IFilterAction {
        private DateTime matcher;

        public void Configure(string data) {
            matcher = DateTime.Parse(data, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
        }

        public void Apply(ref IEnumerable<Screenshot> screenshots) {
            screenshots = from t in screenshots
                          where File.GetLastWriteTime(t.Source.AbsolutePath) < matcher
                          select t;
        }
    }
}
