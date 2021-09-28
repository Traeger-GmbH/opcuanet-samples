// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes.Complex
{
    using System;
    using Opc.UaFx;

    public class MachineJobNode : OpcDataVariableNode<MachineJob>
    {
        #region ---------- Public constructors ----------

        public MachineJobNode(IOpcNode parent, OpcName name, MachineJob value)
            : base(parent, name, value)
        {
            this.Number = this.CreateFieldNode(
                    nameof(MachineJob.Number),
                    () => this.Value.Number,
                    (value) => this.Value.Number = value);

            this.Duration = this.CreateFieldNode(
                    nameof(MachineJob.Duration),
                    () => this.Value.Duration,
                    (value) => this.Value.Duration = value);

            this.EstimatedDuration = this.CreateFieldNode(
                    nameof(MachineJob.EstimatedDuration),
                    () => this.Value.EstimatedDuration,
                    (value) => this.Value.EstimatedDuration = value);

            this.InProcess = this.CreateFieldNode(
                    nameof(MachineJob.InProcess),
                    () => this.Value.InProcess,
                    (value) => this.Value.InProcess = value);

            this.RequiredSetup = this.CreateFieldNode(
                    nameof(MachineJob.RequiredSetup),
                    () => this.Value.RequiredSetup,
                    (value) => this.Value.RequiredSetup = value);

            this.ScheduleTime = this.CreateFieldNode(
                    nameof(MachineJob.ScheduleTime),
                    () => this.Value.ScheduleTime,
                    (value) => this.Value.ScheduleTime = value);
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcDataVariableNode<string> Number
        {
            get;
        }

        public OpcDataVariableNode<int> Duration
        {
            get;
        }

        public OpcDataVariableNode<int> EstimatedDuration
        {
            get;
        }

        public OpcDataVariableNode<bool> InProcess
        {
            get;
        }

        public OpcDataVariableNode<MachineSetup> RequiredSetup
        {
            get;
        }

        public OpcDataVariableNode<DateTime> ScheduleTime
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        protected override OpcVariableValue<object> WriteVariableValueCore(OpcWriteVariableValueContext context, OpcVariableValue<object> value)
        {
            if (value.Value is MachineJob typedValue) {
                this.Number.Value = typedValue.Number;
                this.Duration.Value = typedValue.Duration;
                this.EstimatedDuration.Value = typedValue.EstimatedDuration;
                this.InProcess.Value = typedValue.InProcess;
                this.RequiredSetup.Value = typedValue.RequiredSetup;
                this.ScheduleTime.Value = typedValue.ScheduleTime;
            }

            return base.WriteVariableValueCore(context, value);
        }

        #endregion
    }
}
