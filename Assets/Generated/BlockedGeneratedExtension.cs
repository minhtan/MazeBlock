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

        static readonly Blocked blockedComponent = new Blocked();

        public bool isBlocked {
            get { return HasComponent(ComponentIds.Blocked); }
            set {
                if(value != isBlocked) {
                    if(value) {
                        AddComponent(ComponentIds.Blocked, blockedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Blocked);
                    }
                }
            }
        }

        public Entity IsBlocked(bool value) {
            isBlocked = value;
            return this;
        }
    }

    public partial class Matcher {

        static IMatcher _matcherBlocked;

        public static IMatcher Blocked {
            get {
                if(_matcherBlocked == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Blocked);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBlocked = matcher;
                }

                return _matcherBlocked;
            }
        }
    }
}
