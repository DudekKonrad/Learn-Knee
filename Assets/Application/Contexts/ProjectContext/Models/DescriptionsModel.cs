﻿using System.Collections.Generic;
using Application.ProjectContext.Configs;
using UnityEngine;
using Zenject;

namespace Application.ProjectContext.Models
{
    public class DescriptionsModel
    {
        [Inject] private LearnGameConfig _gameConfig;
        
        private Dictionary<string, string> _descriptions = new Dictionary<string, string>();
        
        public Dictionary<string, string> Descriptions => _descriptions;

        [Inject]
        private void Construct()
        {
            LoadDescriptions();
        }
        
        private void LoadDescriptions()
        {
            var dataLines = _gameConfig.Descriptions.text.Split('\n');

            for (var j = 1; j < dataLines.Length; j++)
            {
                var line = dataLines[j];
                var data = line.Split(';');
                for (var i = 0; i < data.Length - 1; i++)
                {
                    _descriptions.Add(data[i], data[i + 1]);
                }
            }
        }
    }
}