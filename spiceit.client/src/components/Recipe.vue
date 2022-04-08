<template>
  <div class="component">
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
</style>