using System.Numerics;

namespace RaylibJunk2.Components
{
    internal class Transform
    {
        public Transform? parent;
        public List<Transform> children = new List<Transform>();

        Vector2 localPosition;
        Vector2 globalPosition;
        float rotation;
        bool dirty;

        public Vector2 LocalPosition
        {
            get { return localPosition; }
            set { localPosition = value; MarkDirty(); }
        }

        public Vector2 GlobalPosition
        {
            get
            {
                if (dirty)
                    UpdateTransform();

                return globalPosition;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; MarkDirty(); }
        }

        public Transform()
        {
            localPosition = new Vector2(0, 0);
            rotation = 0;
            dirty = true;
        }

        public Transform(Vector2 position, float rotation)
        {
            localPosition = position;
            this.rotation = rotation;
            dirty = true;
        }

        public Transform(Vector2 position)
        {
            localPosition = position;
            rotation = 0;
            dirty = true;
        }

        public Transform(float rotation)
        {
            localPosition = new Vector2(0, 0);
            this.rotation = rotation;
            dirty = true;
        }

        public Transform(Transform? parent) : base()
        {
            parent?.AddChild(this);
            this.parent = parent;
        }

        public void AddChild(Transform child)
        {
            children.Add(child);
            child.parent = this;
        }

        public void RemoveChild(Transform child)
        {
            children.Remove(child);
            child.parent = null;
        }


        private void UpdateTransform()
        {
            if (parent != null)
            {
                // ToDo: Add rotation update



                globalPosition = parent.GlobalPosition + localPosition;
            }
            else
                globalPosition = localPosition;

            dirty = false;

            for (int i = 0; i < children.Count; i++)
            {
                children[i].MarkDirty();
            }
        }

        private void MarkDirty()
        {
            if (!dirty)
            {
                dirty = true;
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].MarkDirty();
                }
            }
        }
    }
}
