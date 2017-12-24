using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Shape
    {
        public bool[] Mask { get; set; }
    }
    public class ShapesGenerator
    {
        List<Shape> shapes;
        string[][] masks;
        public ShapesGenerator()
        {
            InitMasks();
            shapes = new List<Shape>();
            for (int i = 0; i < masks.Length; ++i)
            {
                bool[] elements = new bool[9];
                for (int j = 0; j < masks[i].Length; ++j)
                {
                    for (int el = 0; el < masks[i][j].Length; ++el)
                        elements[el + j * masks[i].Length] = (masks[i][j][el] == '*');
                }
                shapes.Add(new Shape { Mask = elements });
            }
        }
        public Shape GenShape()
        {
            return shapes[Random.Range(0, shapes.Count - 1)];
        }
        void InitMasks()
        {
            masks = new string[][]
            {
            new string []
            {
                "__*" ,
                "__*" ,
                "_**"
            },
            new string[]
            {
                "_*_" ,
                "_*_" ,
                "_*_"
            },
            new string[]
            {
                "_*_" ,
                "***" ,
                "_*_"
            },
            new string[]
            {
                "___" ,
                "***" ,
                "___"
            },
            new string[]
            {
                "***" ,
                "*__" ,
                "___"
            },
            new string[]
            {
                "*__" ,
                "*__" ,
                "**_"
            },
            new string[]
            {
                "___" ,
                "__*" ,
                "***"
            },
            new string[]
            {
                "___" ,
                "_**" ,
                "_**"
            },
            new string[]
            {
                "***",
                "_*_",
                "___"
            },
            new string[]
            {
                "*__",
                "**_",
                "*__"
            },
            new string[]
            {
                "___",
                "_*_",
                "***"
            },
            new string[]
            {
                "__*",
                "_**",
                "__*"
            },
            new string[]
            {
                "**_",
                "_**",
                "___"
            },
            new string[]
            {
                "__*",
                "_**",
                "_*_"
            }
            };
        }
    }
}