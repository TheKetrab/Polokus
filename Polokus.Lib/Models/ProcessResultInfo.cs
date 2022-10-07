using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Lib.Models
{
    public enum ProcessResultState
    {
        Success,
        Failure,
        Suspension
    }
    public class ProcessResultInfo
    {
        public ProcessResultState State { get; set; }
        public IEnumerable<Sequence> SequencesToInvoke { get; set; }
        public string? Message { get; set; }

        public ProcessResultInfo(ProcessResultState state, IEnumerable<Sequence>? sequencesToInvoke = null, string? message = null)
        {
            State = state;
            SequencesToInvoke = sequencesToInvoke ?? Enumerable.Empty<Sequence>();
            Message = message;
        }

        public ProcessResultInfo(ProcessResultState state, string message)
        {
            State = state;
            Message = message;
            SequencesToInvoke = new List<Sequence>();
        }

        public ProcessResultInfo(ProcessResultState state, params Sequence[] sequences)
            : this(state,sequences.AsEnumerable())
        {

        }
    }
}
