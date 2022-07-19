using System;
using System.Collections.Generic;
using System.Linq;

namespace Unit05.Game.Casting
{
    /// <summary>
    /// <para>A long limbless reptile.</para>
    /// <para>The responsibility of Snake is to move itself.</para>
    /// </summary>
    public class Snake2 : Actor
    {
        private List<Actor> segments1 = new List<Actor>();

        /// <summary>
        /// Constructs a new instance of a Snake.
        /// </summary>
        public Snake2()
        {
            PrepareBody();
        }

        /// <summary>
        /// Gets the snake's body segments.
        /// </summary>
        /// <returns>The body segments in a List.</returns>
        public List<Actor> GetBody()
        {
            return new List<Actor>(segments1.Skip(1).ToArray());
        }

        /// <summary>
        /// Gets the snake's head segment.
        /// </summary>
        /// <returns>The head segment as an instance of Actor.</returns>
        public Actor GetHead()
        {
            return segments1[0];
        }

        /// <summary>
        /// Gets the snake's segments (including the head).
        /// </summary>
        /// <returns>A list of snake segments as instances of Actors.</returns>
        public List<Actor> GetSegments()
        {
            return segments1;
        }

        /// <summary>
        /// Grows the snake's tail by the given number of segments.
        /// </summary>
        /// <param name="numberOfSegments">The number of segments to grow.</param>
        public void GrowTail(int numberOfSegments)
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                Actor tail = segments1.Last<Actor>();
                Point velocity = tail.GetVelocity();
                Point offset = velocity.Reverse();
                Point position = tail.GetPosition().Add(offset);

                Actor segment1 = new Actor();
                segment1.SetPosition(position);
                segment1.SetVelocity(velocity);
                segment1.SetText("V");
                segment1.SetColor(Constants.RED);
                segments1.Add(segment1);
            }
        }

        /// <inheritdoc/>
        public override void MoveNext()
        {
            foreach (Actor segment1 in segments1)
            {
                segment1.MoveNext();
            }

            for (int i = segments1.Count - 1; i > 0; i--)
            {
                Actor trailing = segments1[i];
                Actor previous = segments1[i - 1];
                Point velocity = previous.GetVelocity();
                trailing.SetVelocity(velocity);
            }
        }

        /// <summary>
        /// Turns the head of the snake in the given direction.
        /// </summary>
        /// <param name="velocity">The given direction.</param>
        public void TurnHead(Point direction)
        {
            segments1[0].SetVelocity(direction);
        }

        /// <summary>
        /// Prepares the snake body for moving.
        /// </summary>
        private void PrepareBody()
        {
            int x = Constants.MAX_X / 5;
            int y = Constants.MAX_Y / 7;

            for (int i = 0; i < Constants.SNAKE_LENGTH; i++)
            {
                Point position = new Point(x - i * Constants.CELL_SIZE, y);
                Point velocity = new Point(1 * Constants.CELL_SIZE, 0);
                string text = i == 0 ? "8" : "#";
                Color color = i == 0 ? Constants.YELLOW : Constants.GREEN;

                Actor segment1 = new Actor();
                segment1.SetPosition(position);
                segment1.SetVelocity(velocity);
                segment1.SetText(text);
                segment1.SetColor(color);
                segments1.Add(segment1);
            }
        }
    }
}