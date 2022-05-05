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
        <h3><a class="greentext selectable" @click="getMine()">My Posts</a></h3>
      </div>
    </div>

    <div class="row">
      <div class="col-md-2 col-10" v-for="r in recipes" :key="r.id">
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
  <Modal id="create-recipe">
    <template #title> Create Recipe</template>
    <template #body><CreateRecipe :recipeData="recipe" /></template>
  </Modal>
  <Modal id="more-details">
    <template #title>
      <h2>
        {{ active.title }}
        <i
          title="delete ingredient"
          @click="deleteRecipe(active.id)"
          class="mdi mdi-delete text-danger"
        ></i></h2
    ></template>
    <template #body>
      <div class="container-fluid">
        <div class="row bg-white text-dark rounded">
          <!-- <div class="col-10" v-for="i in ingredients" :key="i.id">
            <h1>{{ i.name }}</h1>
          </div> -->
          <h2>The ingredients</h2>
          <div class="col-10 text-dark" v-for="i in ingredients" :key="i.id">
            <h3>{{ i.name }}: {{ i.quantity }}</h3>
            <i
              class="mdi mdi-delete selectable"
              @click="deleteIngredient(i.id)"
            ></i>
            <i
              class="mdi mdi-pencil selectable"
              data-bs-toggle="modal"
              data-bs-target="#edit"
              @click="activeIngred(i.id)"
            ></i>
          </div>
          <CreateIngredient :idata="active" />
          <h2>The steps for the recipe</h2>
          <div class="col-10 text-info" v-for="s in steps" :key="s.id">
            <h3>{{ s.ordr }}: {{ s.body }}</h3>
            <i class="mdi mdi-delete selectable" @click="deleteStep(s.id)"></i>
            <i
              class="mdi mdi-pencil selectable"
              data-bs-toggle="modal"
              data-bs-target="#edit-step"
              @click="activeStep(s.id)"
            ></i>
          </div>
          <CreateStep :sdata="active" />
        </div>
        <!-- <div class="row bg-white text-dark rounded mt-2">
          
        </div> -->
      </div>
    </template>
  </Modal>
  <Modal id="edit">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-10">
            {{ activei.name }}
          </div>
          <EditIngredient />
        </div>
      </div>
    </template>
  </Modal>
  <Modal id="edit-step">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-10">
            {{ actives.body }}
          </div>
          <EditStep />
        </div>
      </div>
    </template>
  </Modal>
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
export default {
  name: 'Home',
  setup() {
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
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
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
      async activeIngred(id) {
        try {
          await ingredientsService.getOne(id)
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
      async activeStep(id) {
        try {
          await stepsService.getOne(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteRecipe(id) {
        try {
          if (await Pop.confirm()) {
            await recipesService.deleteRecipe(id)
            Modal.getOrCreateInstance(document.getElementById('more-details')).hide()
          }

        } catch (error) {
          logger.error
        }
      },
      ingredients: computed(() => AppState.ingredients),
      steps: computed(() => AppState.steps),
      active: computed(() => AppState.activeRecipe),
      activei: computed(() => AppState.activeIngredient),
      actives: computed(() => AppState.activeStep)
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

.greentext {
  color: rgba(33, 150, 83, 1);
}

.navbtn-y {
  transform: translateY(-40%);
}
</style>
