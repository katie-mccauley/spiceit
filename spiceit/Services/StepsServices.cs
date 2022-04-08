using System;
using System.Collections.Generic;
using spiceit.Models;
using spiceit.Repositories;

namespace spiceit.Services
{
  public class StepsServices
  {
    private readonly StepsRepository _steps;

    public StepsServices(StepsRepository steps)
    {
      _steps = steps;
    }

    internal List<Step> GetAll()
    {
      return _steps.GetAll();
    }

    internal Step GetById(int recipeId)
    {
      Step step = _steps.GetById(recipeId);
      if (step == null)
      {
        throw new Exception("Unable to find the ingredient");
      }
      return step;
    }

    internal Step Create(Step stepData)
    {
      return _steps.Create(stepData);
    }
  }
}