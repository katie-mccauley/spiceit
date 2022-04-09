<template>
  <div class="component">
    <div class="row">
      <img
        class="img-fluid cropped rounded"
        :src="recipe.picture"
        alt="recipe picture"
      />
    </div>
    <h2>{{ recipe.title }}</h2>
    <button
      class="btn btn-success"
      data-bs-toggle="modal"
      data-bs-target="#moredetails"
    >
      See more details
    </button>
  </div>
  <Modal id="moredetails">
    <template #title> More Details</template>
    <template #body>
      <div class="container-fluid">
        <div class="row bg-white text-dark rounded">
          <!-- <div class="col-10" v-for="ingredient in ingredients">

          </div> -->
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { ingredientsService } from "../services/IngredientsService"
export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    watchEffect(async () => {
      try {
        await ingredientsService.getAllIngredients(props.recipe.id);
      } catch (error) {
        logger.error(error)
      }
    })
    return {}
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
</style>