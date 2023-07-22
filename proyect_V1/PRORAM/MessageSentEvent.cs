using Prism.Events;
using PRORAM.Models;
using PRORAM.Models.Shared;
using PRORAM.ServicesTcp;

namespace PRORAM
{
    public class MessageSentEvent: PubSubEvent<TargetAreaModel>
    {

    }

    public class MessageSentEvent<T> : PubSubEvent<T>
    {

    }
    public class MsmSentEvent: PubSubEvent<RadarActions>
    {

    }

    public class SendEventDataSet : PubSubEvent<EventsDataSet>
    {

    }
    public class EventLayers: PubSubEvent<EventsDataSet>
    {

    }
    public class EventPanel: PubSubEvent<DetailPanel>
    {

    }
    public class EventPlots : PubSubEvent<Plots>
    {

    }
    public class EventTargets : PubSubEvent<TargetEvents>
    {

    }
    public class EventTools : PubSubEvent<ToolsSelect> { }


    public class EventDrawPlots : PubSubEvent<ActionPlots> { }

}

