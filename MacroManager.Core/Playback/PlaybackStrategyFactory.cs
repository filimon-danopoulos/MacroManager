using MacroManager.Core.Data.Actions;
using MacroManager.Core.Playback.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback
{
    public class PlaybackStrategyFactory
    {
        #region Fields

        private IDictionary<Type, Type> availableStrategies;

        #endregion

        #region Constructors

        public PlaybackStrategyFactory()
        {
            this.Initialize();
        }

        #endregion

        #region Public Methods

        public PlaybackStrategy Create(UserAction action)
        {
            var actionType = action.GetType();
            if (this.availableStrategies.ContainsKey(actionType))
            {
                return (PlaybackStrategy)Activator.CreateInstance(this.availableStrategies[actionType]);
            }
            return new UnkownStrategy();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (this.availableStrategies == null)
            {
                this.availableStrategies = new Dictionary<Type, Type>();
            }

            var assembly = Assembly.GetExecutingAssembly();
            var relateActionAttributeType =  typeof(PlaybackStrategy.ExecutesAttribute);
            var strategies = assembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(PlaybackStrategy)) && 
                    x.CustomAttributes.Any(y => y.AttributeType == relateActionAttributeType)
                );
            foreach (var strategy in strategies) {
                var relatedAction = (PlaybackStrategy.ExecutesAttribute) Attribute.GetCustomAttribute(strategy, relateActionAttributeType);
                this.availableStrategies.Add(relatedAction.ActionType, strategy);
            }
        }

        #endregion

    }
}
