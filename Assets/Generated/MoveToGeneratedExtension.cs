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

        public MoveTo moveTo { get { return (MoveTo)GetComponent(ComponentIds.MoveTo); } }
        public bool hasMoveTo { get { return HasComponent(ComponentIds.MoveTo); } }

        public Entity AddMoveTo(Entitas.Entity newNode) {
            var component = CreateComponent<MoveTo>(ComponentIds.MoveTo);
            component.node = newNode;
            return AddComponent(ComponentIds.MoveTo, component);
        }

        public Entity ReplaceMoveTo(Entitas.Entity newNode) {
            var component = CreateComponent<MoveTo>(ComponentIds.MoveTo);
            component.node = newNode;
            ReplaceComponent(ComponentIds.MoveTo, component);
            return this;
        }

        public Entity RemoveMoveTo() {
            return RemoveComponent(ComponentIds.MoveTo);
        }
    }

    public partial class Matcher {

        static IMatcher _matcherMoveTo;

        public static IMatcher MoveTo {
            get {
                if(_matcherMoveTo == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MoveTo);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMoveTo = matcher;
                }

                return _matcherMoveTo;
            }
        }
    }
}
