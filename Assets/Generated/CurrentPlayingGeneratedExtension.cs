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

        public CurrentPlaying currentPlaying { get { return (CurrentPlaying)GetComponent(ComponentIds.CurrentPlaying); } }
        public bool hasCurrentPlaying { get { return HasComponent(ComponentIds.CurrentPlaying); } }

        public Entity AddCurrentPlaying(Player newPlayer) {
            var component = CreateComponent<CurrentPlaying>(ComponentIds.CurrentPlaying);
            component.player = newPlayer;
            return AddComponent(ComponentIds.CurrentPlaying, component);
        }

        public Entity ReplaceCurrentPlaying(Player newPlayer) {
            var component = CreateComponent<CurrentPlaying>(ComponentIds.CurrentPlaying);
            component.player = newPlayer;
            ReplaceComponent(ComponentIds.CurrentPlaying, component);
            return this;
        }

        public Entity RemoveCurrentPlaying() {
            return RemoveComponent(ComponentIds.CurrentPlaying);
        }
    }

    public partial class Pool {

        public Entity currentPlayingEntity { get { return GetGroup(Matcher.CurrentPlaying).GetSingleEntity(); } }
        public CurrentPlaying currentPlaying { get { return currentPlayingEntity.currentPlaying; } }
        public bool hasCurrentPlaying { get { return currentPlayingEntity != null; } }

        public Entity SetCurrentPlaying(Player newPlayer) {
            if(hasCurrentPlaying) {
                throw new EntitasException("Could not set currentPlaying!\n" + this + " already has an entity with CurrentPlaying!",
                    "You should check if the pool already has a currentPlayingEntity before setting it or use pool.ReplaceCurrentPlaying().");
            }
            var entity = CreateEntity();
            entity.AddCurrentPlaying(newPlayer);
            return entity;
        }

        public Entity ReplaceCurrentPlaying(Player newPlayer) {
            var entity = currentPlayingEntity;
            if(entity == null) {
                entity = SetCurrentPlaying(newPlayer);
            } else {
                entity.ReplaceCurrentPlaying(newPlayer);
            }

            return entity;
        }

        public void RemoveCurrentPlaying() {
            DestroyEntity(currentPlayingEntity);
        }
    }

    public partial class Matcher {

        static IMatcher _matcherCurrentPlaying;

        public static IMatcher CurrentPlaying {
            get {
                if(_matcherCurrentPlaying == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CurrentPlaying);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCurrentPlaying = matcher;
                }

                return _matcherCurrentPlaying;
            }
        }
    }
}
