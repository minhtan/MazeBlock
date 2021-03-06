//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {

    public partial class Entity {

        public CoroutineQueue coroutineQueue { get { return (CoroutineQueue)GetComponent(ComponentIds.CoroutineQueue); } }
        public bool hasCoroutineQueue { get { return HasComponent(ComponentIds.CoroutineQueue); } }

        public Entity AddCoroutineQueue(System.Collections.Generic.Queue<System.Collections.IEnumerator> newQueue) {
            var component = CreateComponent<CoroutineQueue>(ComponentIds.CoroutineQueue);
            component.Queue = newQueue;
            return AddComponent(ComponentIds.CoroutineQueue, component);
        }

        public Entity ReplaceCoroutineQueue(System.Collections.Generic.Queue<System.Collections.IEnumerator> newQueue) {
            var component = CreateComponent<CoroutineQueue>(ComponentIds.CoroutineQueue);
            component.Queue = newQueue;
            ReplaceComponent(ComponentIds.CoroutineQueue, component);
            return this;
        }

        public Entity RemoveCoroutineQueue() {
            return RemoveComponent(ComponentIds.CoroutineQueue);
        }
    }

    public partial class Matcher {

        static IMatcher _matcherCoroutineQueue;

        public static IMatcher CoroutineQueue {
            get {
                if(_matcherCoroutineQueue == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CoroutineQueue);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCoroutineQueue = matcher;
                }

                return _matcherCoroutineQueue;
            }
        }
    }
}
