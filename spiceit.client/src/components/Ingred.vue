<template></template>


<script>
import { watchEffect } from "@vue/runtime-core"
import { ingredientsService } from "../services/IngredientsService"
export default {
  setup() {
    watchEffect(async () => {
      await ingredientsService.getAllIngredients()
    })
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
</style>