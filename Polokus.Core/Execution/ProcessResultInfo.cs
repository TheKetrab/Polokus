﻿namespace Polokus.Core.Execution
{


    public class SuccessProcessResultInfo : ProcessResultInfo
    {
        public SuccessProcessResultInfo(IEnumerable<ISequence>? sequencesToInvoke = null, string? message = null)
            : base(ProcessResultState.Success, message)
        {
            SequencesToInvoke = sequencesToInvoke ?? Enumerable.Empty<ISequence>();
        }

        public SuccessProcessResultInfo(params ISequence[] sequences)
            : this(sequences.AsEnumerable())
        {

        }
    }

    public class FailureProcessResultInfo : ProcessResultInfo
    {
        public FailureProcessResultInfo(string? message = null)
            : base(ProcessResultState.Failure, message)
        {

        }
    }

    public class ProcessResultInfo : IProcessResultInfo
    {
        public ProcessResultState State { get; protected set; }
        public IEnumerable<ISequence>? SequencesToInvoke { get; protected set; }
        public string? Message { get; protected set; }

        public ProcessResultInfo(ProcessResultState state, string? message = null)
        {
            State = state;
            Message = message;
        }


    }
}
