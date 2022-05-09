<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div
        class="
          col-md-4 col-10
          bg-white
          p-3
          shadow
          rounded
          d-flex
          justify-content-around
          elevation-3
          navbtn-y
        "
      >
        <h3><a class="greentext selectable" @click="getAll()">ALL</a></h3>
        <h3>
          <a class="greentext selectable" @click="getFavs()">Favorites</a>
        </h3>
        <h3>
          <a class="greentext selectable" @click="getMine()">My Recipes</a>
        </h3>
      </div>
    </div>

    <div class="row">
      <div class="col-md-2 col-12" v-for="r in recipes" :key="r.id">
        <!-- <div class="row justify-content-end">
          <button
            @click="deleteRecipe(r.id)"
            type="button"
            title="Delete Recipe"
            class="btn-close btn-close-dark me-2"
            aria-label="Close"
          ></button>
        </div> -->

        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
  <ModalForm id="create-recipe">
    <template #title> Create Recipe</template>
    <template #body><CreateRecipe :recipeData="recipe" /></template>
  </ModalForm>
  <RecipeDetails />
  <ModalForm id="edit">
    <template #title> Edit Ingredient</template>
    <template #body>
      <div class="container-fluid">
        <div class="row">
          <!-- <div class="col-10">
            {{ activei.name }}
          </div> -->
          <EditIngredient :edit="activei" />
        </div>
      </div>
    </template>
  </ModalForm>
  <ModalForm id="edit-step">
    <template #title>Edit Step</template>
    <template #body>
      <div class="container-fluid">
        <div class="row">
          <!-- <div class="col-10">
            {{ actives.body }}
          </div> -->
          <EditStep :editStep="actives" />
        </div>
      </div>
    </template>
  </ModalForm>
</template>

<script>
import { computed, onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import { favoritesService } from "../services/FavoritesService"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { ingredientsService } from "../services/IngredientsService"
import { stepsService } from "../services/StepsService"
import { Modal } from "bootstrap"
import { useRouter } from "vue-router"
import { pinsService } from "../services/PinsService"
export default {
  name: 'Home',
  setup() {
    const router = useRouter()
    watchEffect(async () => {
      try {
        await recipesService.getAllRecipes()
        await recipesService.getFavs()
        await recipesService.getMine()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      recipes: computed(() => AppState.recipes),
      async getAll() {
        try {
          await recipesService.getAllRecipes()
        } catch (error) {
          logger.error(error)
        }
      },
      async getFavs() {
        try {
          await favoritesService.getAll()
        } catch (error) {
          logger.error(error)

        }
      },
      async getMine() {
        try {
          await recipesService.getMine()
        } catch (error) {
          logger.error(error)

        }
      },
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

* {
  font-family: Sahitya;
}

.bg-g {
  background-color: #e9eaec;
}

.cropped {
  height: 50px;
  max-width: 50px;
  border-radius: 50%;
}

.greentext {
  color: rgba(33, 150, 83, 1);
}

.navbtn-y {
  transform: translateY(-40%);
}

.heightimg {
  height: 400px;
}

.object-fit-cover {
  object-fit: cover;
}
</style>
