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

    internal List<Step> GetSteps(int recipeId)
    {
      return _steps.GetSteps(recipeId);
      // if (step == null)
      // {
      //   throw new Exception("Unable to find the ingredient");
      // }

    }

    internal Step Create(Step stepData)
    {
      return _steps.Create(stepData);
    }

    internal void RemoveStep(int id, string userId)
    {
      Step step = GetStepById(id);
      if (step.CreatorId != userId)
      {
        throw new Exception("unable to find the ingredient");
      }
      _steps.RemoveStep(id);
    }

    internal Step Update(Step stepData, string userId)
    {
      Step step = GetStepById(stepData.Id);
      if (step.CreatorId != userId)
      {
        throw new Exception("you can't do that nice try.");
      }
      step.Ordr = stepData.Ordr;
      step.Body = stepData.Body;
      _steps.Update(step);
      return step;
    }

    internal Step GetStepById(int id)
    {
      Step step = _steps.GetStepById(id);
      if (step == null)
      {
        throw new Exception("unable to find the ingredient");

      }
      return step;
    }
  }
}