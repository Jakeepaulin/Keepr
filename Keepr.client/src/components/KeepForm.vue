<template>
  <form @submit.prevent="createKeep()">
    <div class="modal-body">
      <div class="form-floating mb-3">
        <input
          v-model="editable.name"
          required
          type="text"
          class="form-control"
          id="keepName"
          placeholder="Name..."
        />
        <label for="keepName">Keep Name</label>
      </div>
      <div class="form-floating">
        <textarea
          v-model="editable.description"
          required
          type="text"
          class="form-control"
          id="keepDescription"
          placeholder="Description..."
        >
        </textarea>
        <label for="floatingPassword">Description</label>
      </div>
      <div class="form-floating mb-3">
        <input
          v-model="editable.img"
          required
          type="url"
          class="form-control"
          id="keepImg"
          placeholder="Image..."
        />
        <label for="keepImg">Keep Image</label>
      </div>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
        Close
      </button>
      <button type="submit" class="btn btn-primary">Make this Keep!</button>
    </div>
  </form>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
