export class Keep {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    this.creator = data.creator;
    this.creatorId = data.creatorId;
    this.description = data.description;
    this.img = data.img;
    this.views = data.views;
    this.kept = data.kept;
    this.keeps = data.keeps;
    this.vaultKeepId = data.vaultKeepId;
  }
}
