using System.Threading.Tasks;
using Actions;
using Interfaces;
using Items;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IEntity, IProcessable
    {
        [SerializeField] private EntityStatistics statistics = new EntityStatistics();
        
        public EntityAction CurrentAction { get; private set; }
        public EntityStatistics Statistics => statistics;

        public EntityMovement Movement { get; private set; }
        public IItemContainer Container { get; private set; }
        public ItemHolder ItemHolder { get; private set; }

        private Coroutine actionCoroutine;

        protected void Awake()
        {
            Movement = GetComponentInChildren<EntityMovement>();
            Container = GetComponentInChildren<IItemContainer>();
            ItemHolder = GetComponentInChildren<ItemHolder>();
        }

        protected void Start()
        {
            //ProgressManager.Current.RegisterProgressBar(EntityActionQueue, transform);
            MonoBehaviourManager.Current.RegisterProcessable(this);
        }

        private async void StartWork()
        {
            if (actionCoroutine != null) return;
            
            if (CurrentAction == null) 
                SearchForWork();
            if (CurrentAction == null) return;
            actionCoroutine = StartCoroutine(CurrentAction.WorkCoroutine(this, () =>
            {
                CurrentAction = null;
                actionCoroutine = null;
            }));
        }
        
        public virtual void Process()
        {
            StartWork();
        }

        private void SearchForWork()
        {
            foreach (var tile in MapManager.Current.Grid.GetSortedActiveTilesByDistance(transform.position))
            {
                foreach (var actionNode in tile.ActionNodes)
                {
                    if (actionNode.TryGetAction(this, out var action))
                    {
                        CurrentAction = action;
                        return;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (CurrentAction == null) return;
            GUIStyle debugStyle = new GUIStyle
                { normal = new GUIStyleState { textColor = Color.black }, fontSize = 20 };
            Handles.Label(transform.position, CurrentAction.ToString(), debugStyle);
        }
    }
}