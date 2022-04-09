<template>
  <div class="component">
    <div class="row">
      <img
        class="img-fluid cropped"
        :src="recipe.picture"
        alt="recipe picture"
      />
    </div>
    <h2>{{ recipe.title }}</h2>
  </div>
</template>


<script>
export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    }
  },
  setup() {
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