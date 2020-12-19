﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Lab2
{
    class V5DataCollection : V5Data, IEnumerable
    {
        public Dictionary<Vector2, Vector2> ValuesDct { get; set; } =
            new Dictionary<Vector2, Vector2>();

        public V5DataCollection(string serviceInfo, DateTime measurementTime) :
            base(serviceInfo, measurementTime)
        { }

        public V5DataCollection(V5DataOnGrid v5DataOnGridInstance) :
            base(v5DataOnGridInstance.ServiceInfo, v5DataOnGridInstance.MeasurementTime)
        {
            var iterV5DataOnGrid = from lstElem in v5DataOnGridInstance
                                   select new KeyValuePair<Vector2, Vector2>
                                   (lstElem.PointCoord, lstElem.FieldValue);

            ValuesDct = new Dictionary<Vector2, Vector2>(iterV5DataOnGrid);
        }

        public void InitRandom(int nItems, float xmax, float ymax, float minValue, float maxValue)
        {
            Random rand = new Random();
            for (int i = 0; i < nItems; ++i)
            {
                float x = (float)rand.NextDouble() * xmax;
                float y = (float)rand.NextDouble() * ymax;
                float valueX = minValue + (float)rand.NextDouble() * (maxValue - minValue);
                float valueY = minValue + (float)rand.NextDouble() * (maxValue - minValue);
                ValuesDct[new Vector2(x, y)] = new Vector2(valueX, valueY);
            }
        }

        public override Vector2[] NearEqual(float eps)
        {
            List<Vector2> NearValues = new List<Vector2>();
            foreach (Vector2 value in ValuesDct.Values)
            {
                if (Math.Abs(value.X - value.Y) < eps)
                {
                    NearValues.Add(value);
                }
            }
            return NearValues.ToArray();
        }

        public override string ToString()
        {
            return $"Type: V5DataCollection\n{base.ToString()}\n" +
                $"Number of measurements: {ValuesDct.Count}";
        }

        public override string ToLongString()
        {
            StringBuilder strBulder = new StringBuilder(ToString() + "\n");

            foreach (Vector2 key in ValuesDct.Keys)
            {
                strBulder.Append($"x = {key.X}  y = {key.Y}: {ValuesDct[key]}\n");
            }

            return strBulder.ToString();
        }

        public override string ToLongString(string format)
        {
            StringBuilder strBulder = new StringBuilder(ToString() + "\n");

            foreach (Vector2 key in ValuesDct.Keys)
            {
                strBulder.Append($"x = {key.X.ToString(format)}  y = {key.Y.ToString(format)}: " +
                    $"{ValuesDct[key].ToString(format)}\n");
            }

            return strBulder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            foreach (KeyValuePair<Vector2, Vector2> kvp in ValuesDct)
            {
                yield return new DataItem(kvp.Key, kvp.Value);
            }
        }
    }
}
