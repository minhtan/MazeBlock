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

        public StandOn standOn { get { return (StandOn)GetComponent(ComponentIds.StandOn); } }
        public bool hasStandOn { get { return HasComponent(ComponentIds.StandOn); } }

        public Entity AddStandOn(Entitas.Entity newNode) {
            var component = CreateComponent<StandOn>(ComponentIds.StandOn);
            component.node = newNode;
            return AddComponent(ComponentIds.StandOn, component);
        }

        public Entity ReplaceStandOn(Entitas.Entity newNode) {
            var component = CreateComponent<StandOn>(ComponentIds.StandOn);
            component.node = newNode;
            ReplaceComponent(ComponentIds.StandOn, component);
            return this;
        }

        public Entity RemoveStandOn() {
            return RemoveComponent(ComponentIds.StandOn);
        }
    }

    public partial class Matcher {

        static IMatcher _matcherStandOn;

        public static IMatcher StandOn {
            get {
                if(_matcherStandOn == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.StandOn);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherStandOn = matcher;
                }

                return _matcherStandOn;
            }
        }
    }
}
