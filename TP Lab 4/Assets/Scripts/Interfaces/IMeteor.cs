// This interface is for meteor game objects.
public interface IMeteor
{
    float distanceSquared { get; set; }
    bool isMovingLeft { get; set; }
    float speed { get; set; }
    void Move();
}
