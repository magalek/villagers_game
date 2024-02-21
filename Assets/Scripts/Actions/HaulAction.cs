using Entities;
using Interfaces;

namespace Actions
{
    public class HaulAction : MoveAction
    {
        private IInputNode inputNode;
        private IOutputNode outputNode;

        public HaulAction(IOutputNode _outputNode, IInputNode _inputNode) 
            : base(_outputNode, _inputNode)
        {
            inputNode = _inputNode;
            outputNode = _outputNode;
        }
    }
}