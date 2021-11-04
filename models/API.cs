using System;
using System.Collections.Generic;

namespace aws_stuff.models {

    public interface IAPITemplate {};
    public interface IResultTemplate {
        PersonDTO User { get; set; }
    }
    public interface IRandomAPITemplate : IAPITemplate
    {
        List<ResultTemplate> Results { get; }
        // QUestion: Result or Iresut
    }

    public class ResultTemplate : IResultTemplate
    {
        public PersonDTO User { get; set; }
    }
    public class RandomAPITemplate : IRandomAPITemplate
    {
        public List<ResultTemplate> Results { get; set; }
    }
}