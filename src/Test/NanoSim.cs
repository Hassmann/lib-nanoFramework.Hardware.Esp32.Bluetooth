using System;

namespace nanoFramework.Runtime.Events
{
    public class BaseEvent
    {

    }

    public interface IEventListener
    {
        void InitializeForEventSource();
        bool OnEvent(BaseEvent e);
    }

    public interface IEventProcessor
    {
        BaseEvent ProcessEvent(uint data1, uint data2, DateTime time);
    }

    public enum EventCategory
    {
        Custom,
    }

    public static class EventSink
    {
        static IEventProcessor processor;
        static IEventListener listener;

        public static void AddEventProcessor(EventCategory category, IEventProcessor processor)
        {
            EventSink.processor = processor;
        }

        public static void AddEventListener(EventCategory category, IEventListener listener)
        {
            EventSink.listener = listener;

            listener.InitializeForEventSource();
        }

        internal static void Fire(uint data1, uint data2, DateTime time)
            => Fire(processor.ProcessEvent(data1, data2, time));

        internal static void Fire(BaseEvent e)
            => listener.OnEvent(e);

    }
}