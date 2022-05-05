<template>
  <div class="card selectable border border-3 rounded bg-white">
    <img
      class="img-fluid cropped rounded"
      :src="recipe.picture"
      alt="recipe picture"
    />
    <div
      class="
        card-img-overlay
        d-flex
        flex-column
        justify-content-end
        backgroundfix
        pb-md-2 pb-0
      "
    >
      <!-- <div class="row justify-content-md-around">
        <div class="col-12 d-flex justify-content-end">
          <button
            @click="deleteRecipe(recipe.id)"
            type="button"
            title="Delete Recipe"
            class="btn-close btn-close-white me-2"
            aria-label="Close"
          ></button>
        </div>
      </div> -->
      <div class="row justify-content-md-around">
        <div class="col-10">
          <h2
            data-bs-toggle="modal"
            data-bs-target="#more-details"
            @click="activeRecipe(recipe.id)"
            class="text-light"
          >
            {{ recipe.title }}
          </h2>
        </div>
        <!-- <div class="col-4">
          <button
            class="btn btn-outline-light"
            data-bs-toggle="modal"
            data-bs-target="#more-details"
            @click="activeRecipe(recipe.id)"
          >
            Info
          </button>
        </div> -->
        <div class="col-2">
          <h3 class="m-0">
            <i
              class="mdi mdi-heart text-danger selectable m-0 p-0"
              @click="createFav(recipe.id)"
            ></i>
          </h3>
        </div>
        <!-- <div class="col-5">
          <button
            class="btn btn-success"
            
          >
            See more details
          </button>
        </div> -->
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { ingredientsService } from "../services/IngredientsService"
import { AppState } from "../AppState"
import { stepsService } from "../services/StepsService"
import { recipesService } from "../services/RecipesService"
import { favoritesService } from "../services/FavoritesService"
import Pop from "../utils/Pop"
export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    // watchEffect(async () => {
    //   try {

    //   } catch (error) {
    //     logger.error(error)
    //   }
    // })
    return {
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      account: computed(() => AppState.account),
      async deleteStep(id) {
        try {
          await stepsService.deleteStep(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteIngredient(id) {
        try {
          await ingredientsService.deleteIngredient(id)
        } catch (error) {
          logger.error(error)
          Pop.toast('error')
        }
      },
      async activeRecipe(id) {
        try {
          logger.log(id, props.recipe)
          await recipesService.activeRecipe(id)
          await stepsService.getAllSteps(id);
          await ingredientsService.getAllIngredients(id);
        } catch (error) {
          logger.error(error)
        }
      },
      async createFav(id) {
        let fav = {
          accountId: AppState.account.id,
          recipeId: id,

        }

        await favoritesService.createFav(fav)
      },
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.cropped {
  height: 300px;
  position: center;
  display: cover;
  object-fit: cover;
}

.backgroundfix {
  background: linear-gradient(
    180.45deg,
    rgba(0, 0, 0, 0) 67.72%,
    rgba(0, 0, 0, 0.53) 99.61%
  );
}
</style>