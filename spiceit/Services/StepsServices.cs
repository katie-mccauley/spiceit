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

    internal Step GetSteps(int recipeId)
    {
      Step step = _steps.GetSteps(recipeId);
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

    internal void RemoveStep(int recipeId, string id)
    {
      Step step = GetSteps(recipeId);
      _steps.RemoveStep(recipeId);
    }

    internal Step Update(Step stepData)
    {
      Step step = GetSteps(stepData.RecipeId);
      step.Ordr = stepData.Ordr;
      step.Body = stepData.Body;
      _steps.Update(step);
      return step;
    }
  }
}