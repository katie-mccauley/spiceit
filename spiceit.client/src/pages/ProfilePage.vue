<template>
  <div class="container-fluid"></div>
</template>


<script>
import { watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { boardsService } from "../services/BoardsService"
import { recipesService } from "../services/RecipesService"
import { useRoute } from "vue-router"
export default {
  setup() {
    const route = useRoute()
    watchEffect(async () => {
      try {
        await boardsService.getUserBoards(route.params.id)
        await recipesService.getUserRecipes(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
</style>