namespace Clones.Animation
{
    public static class Animations
    {
        public static class Enemy
        {
            public static class Triggers
            {
                public const string Attack = nameof(Attack);
            }

            public static class Bools
            {
                public const string IsMoved = nameof(IsMoved);
            }
        }

        public static class Player
        {
            public static class Triggers
            {
                public const string Attack = nameof(Attack);
            }

            public static class Bools
            {
                public const string IsMoved = nameof(IsMoved);
            }

            public static class Floats
            {
                public const string MovementAnimationSpeed = nameof(MovementAnimationSpeed);
            }
        }
    }
}
