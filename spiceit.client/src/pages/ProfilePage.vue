<template>
  <div class="container-fluid">
    <div class="row m-3 ms-0">
      <div class="col-md-1 col-3">
        <div class="card">
          <img :src="profile.picture" class="img-fluid" alt="" />
        </div>
      </div>
      <div class="col-md-5 col-7">
        <h1>{{ profile.name }}</h1>
        <h3>Recipes: {{ recipes.length }}</h3>
        <h3>Boards: {{ boards.length }}</h3>
      </div>
    </div>
    <div class="row">
      <h2>Boards</h2>
      <div class="col-md-3" v-for="b in boards" :key="b.id">
        <Board :board="b" />
      </div>
    </div>
    <div class="row">
      <h2>Recipes</h2>
      <div class="col-md-3" v-for="r in recipes" :key="r.id">
        <Recipe :recipe="r" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { boardsService } from "../services/BoardsService"
import { recipesService } from "../services/RecipesService"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await boardsService.getUserBoards(route.params.id)
        await recipesService.getUserRecipes(route.params.id)
        await recipesService.getUsers(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      profile: computed(() => AppState.profile),
      recipes: computed(() => AppState.recipesUser),
      boards: computed(() => AppState.boardsUser)
    }
  }
}
</script>


<style lang="scss" scoped>
* {
  font-family: Sahitya;
}
</style>