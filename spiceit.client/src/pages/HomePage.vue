<template>
  <div class="container-fluid">
    <div class="row">
      <div
        class="col-6 home-card p-5 bg-white rounded elevation-3"
        v-for="r in recipes"
        :key="r.id"
      >
        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { recipesService } from "../services/RecipesService"
import { AppState } from "../AppState"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAllRecipes()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      recipes: computed(() => AppState.recipes)
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
</style>
