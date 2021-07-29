using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class BuildingLabirint : MonoBehaviour
{
    [Tooltip("Спрайт, на котором нарисован лабиринт")]
    [SerializeField] private Sprite targetSprite;

    private Sprite _lastSprite;
    private bool isBuild = false;

    [Tooltip("Каким цветом закрашены стенки")]
    [SerializeField] private Color wallColor;

    [Tooltip("Каким цветом закрашен задний фон")]
    [SerializeField] private Color backgroundColor;

    [SerializeField] private GameObject prefabWall;
    [SerializeField] private Vector2 startCoordinates;

    [SerializeField] private GameObject mainPlane;
    
    private float _defaultSize = 1f;

    [SerializeField] private GameObject prefabPlayer;
    //[Tooltip("Каким цветом закрашен старт игрока")]
    //[SerializeField] private Color playerColor;

    [Tooltip("Высокие стенки или низкие?")]
    [SerializeField] private bool isHighWalls = false;

    [Tooltip("Меняется, если выше нажата галочка")]
    [SerializeField] private float targetHeightScale;

    //private void Awake()
    //{
    //    if (isBuild == false)
    //    {
    //        InitializationCoordinates();
    //    }
    //}

    //private void Start()
    //{
    //    if (isBuild == false)
    //    {
    //        ReadSprite();
    //    }
    //}

    //private void Update()
    //{
    //    if (isBuild == false)
    //    {
    //        if (_lastSprite == null || _lastSprite != targetSprite)
    //        {
    //            _lastSprite = targetSprite;
    //            isBuild = true;
    //            InitializationCoordinates();
    //            ReadSprite();
    //        }
    //    }
    //}

    private void InitializationCoordinates()
    {
        //if (isHighWalls)
        //{
        //    startCoordinates = new Vector2(Mathf.Round(startCoordinates.x), Mathf.Round(startCoordinates.y));
        //}

        float offsetCamera = 0f;

        float newScale = (targetSprite.texture.width + 10f) / 10f;
        newScale -= _defaultSize;

        mainPlane.transform.localScale = new Vector3(newScale, mainPlane.transform.localScale.y, mainPlane.transform.localScale.z);

        Vector3 offset = new Vector3(5f * (newScale - _defaultSize), 0, 0);
        mainPlane.transform.localPosition = new Vector3(offset.x, mainPlane.transform.localPosition.y, mainPlane.transform.localPosition.z);
        offsetCamera += offset.x;

        newScale = (targetSprite.texture.height + 10f) / 10f;
        newScale -= _defaultSize;

        mainPlane.transform.localScale = new Vector3(mainPlane.transform.localScale.x, mainPlane.transform.localScale.y, newScale);

        offset = new Vector3(0, 0, 5f * (newScale - _defaultSize));
        mainPlane.transform.localPosition = new Vector3(mainPlane.transform.localPosition.x, mainPlane.transform.localPosition.y, offset.z);
        
        
        //offsetCamera += offset.z;
        //Camera.main.transform.position = new Vector3(mainPlane.transform.localPosition.x,
        //  Camera.main.transform.position.y + offsetCamera + offsetCamera / 2f,
        //mainPlane.transform.localPosition.z); //ставить в начало и увеличивать на мЕньшее кол-во
    }

    private void ReadSprite()
    {
        for (int i = 0; i < targetSprite.texture.width; i++) //i = x, j = z
        {
            for (int j = 0; j < targetSprite.texture.height; j++)
            {
                Color pixel = targetSprite.texture.GetPixel(i, j);

                if (pixel.r == wallColor.r && pixel.g == wallColor.g && pixel.b == wallColor.b)
                {
                    if (isHighWalls == false)
                    {
                        CreateWall(i, j);
                    }
                    else
                    {
                        //if(i == )

                        bool isRight = false;
                        if (i+1 != targetSprite.texture.width && targetSprite.texture.GetPixel(i +1, j).r == wallColor.r && targetSprite.texture.GetPixel(i+1, j).g == wallColor.g && targetSprite.texture.GetPixel(i+1, j).b == wallColor.b)
                        {
                            isRight = true;
                        }

                        bool isLeft = false;
                        if (i - 1 != 1 && targetSprite.texture.GetPixel(i - 1, j).r == wallColor.r && targetSprite.texture.GetPixel(i - 1, j).g == wallColor.g && targetSprite.texture.GetPixel(i - 1, j).b == wallColor.b)
                        {
                            isLeft = true;
                        }

                        bool isUp = false;
                        if (j + 1 != targetSprite.texture.height && targetSprite.texture.GetPixel(i, j +1).r == wallColor.r && targetSprite.texture.GetPixel(i, j + 1).g == wallColor.g && targetSprite.texture.GetPixel(i, j + 1).b == wallColor.b)
                        {
                            isUp = true;
                        }

                        bool isDown = false;
                        if (j - 1 != 1 && targetSprite.texture.GetPixel(i, j - 1).r == wallColor.r && targetSprite.texture.GetPixel(i, j - 1).g == wallColor.g && targetSprite.texture.GetPixel(i, j - 1).b == wallColor.b)
                        {
                            isDown = true;
                        }

                        CreateHighWall(i, j, isLeft, isRight, isUp, isDown);
                    }
                } 
                else if(pixel.r != backgroundColor.r && pixel.g != backgroundColor.g && pixel.b != backgroundColor.b)
                {
                    CreatePlayer(i, j);
                }
                //else
                //{
                //    Debug.Log(pixel.r + "   " + pixel.g + "   " + pixel.b + "   ");
                //}

                
            }
        }
    }

    private void CreateHighWall(float targetX, float targetZ, bool isLeft = (default), bool isRight = (default), bool isUp = (default), bool isDown = (default))
    {
        Vector2 targetPosition = new Vector2(startCoordinates.x + targetX, startCoordinates.y + targetZ);
        Vector3 finishPosition = new Vector3(targetPosition.x, 0.5f, targetPosition.y);

        if (isLeft == false && isRight == false && isUp == false && isDown == false)
        {
            GameObject newWall = Instantiate(prefabWall, finishPosition, Quaternion.identity);

            newWall.transform.localScale = new Vector3(0.25f, targetHeightScale, 0.25f);
            newWall.transform.position = new Vector3(newWall.transform.position.x, newWall.transform.localScale.y / 2f, newWall.transform.position.z);
        }
        else
        {
            float offset = 0.125f; //0.125f
            float maxCountWalls = 2;

            if (isDown)
            {
                float startPosition = offset;
                float startPositionConst = 0.25f;

                for (int i = 1; i <= maxCountWalls; i++)
                {
                    Vector3 newPos = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z - startPosition);
                    GameObject newWall = Instantiate(prefabWall, newPos, Quaternion.identity);
                    newWall.transform.localScale = new Vector3(0.25f, targetHeightScale, 0.25f);
                    startPosition += startPositionConst;

                    newWall.transform.parent = mainPlane.transform;
                }
            }

            if (isUp)
            {
                float startPosition = offset;
                float startPositionConst = 0.25f;

                for (int i = 1; i <= maxCountWalls; i++)
                {
                    Vector3 newPos = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z + startPosition);
                    GameObject newWall = Instantiate(prefabWall, newPos, Quaternion.identity);
                    newWall.transform.localScale = new Vector3(0.25f, targetHeightScale, 0.25f);
                    startPosition += startPositionConst;

                    newWall.transform.parent = mainPlane.transform;
                }
            }

            if (isRight)
            {
                float startPosition = offset;
                float startPositionConst = 0.25f;

                for (int i = 1; i <= maxCountWalls; i++)
                {
                    Vector3 newPos = new Vector3(finishPosition.x + startPosition, finishPosition.y, finishPosition.z);
                    GameObject newWall = Instantiate(prefabWall, newPos, Quaternion.identity);
                    newWall.transform.localScale = new Vector3(0.25f, targetHeightScale, 0.25f);
                    startPosition += startPositionConst;

                    newWall.transform.parent = mainPlane.transform;
                }
            }

            if (isLeft)
            {
                float startPosition = offset;
                float startPositionConst = 0.25f;

                for (int i = 1; i <= maxCountWalls; i++)
                {
                    Vector3 newPos = new Vector3(finishPosition.x - startPosition, finishPosition.y, finishPosition.z);
                    GameObject newWall = Instantiate(prefabWall, newPos, Quaternion.identity);
                    newWall.transform.localScale = new Vector3(0.25f, targetHeightScale, 0.25f);
                    startPosition += startPositionConst;

                    newWall.transform.parent = mainPlane.transform;
                }
            }
        }



        //float startPosition = 0.125f;
        //float startPositionConst = 0.25f;
        //int maxCountWalls = 4;

        //for (int i = 1; i <= maxCountWalls; i++)
        //{
        //    Vector3 newFinishPosition = Vector3.zero;
        //    float newPos = Mathf.Floor(finishPosition.z);
        //    if (newPos < 0)
        //    {
        //        newFinishPosition = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z - startPosition);
        //    }
        //    else if (newPos > 0)
        //    {
        //        newFinishPosition = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z + startPosition);
        //    }
        //    else if (newPos == 0)
        //    {
        //        Debug.Log(1111111);
        //        if (i > maxCountWalls / 2f)
        //        {
        //            startPosition -= 2 * startPositionConst;
        //        }

        //        if (i > maxCountWalls / 2f)
        //        {
        //            newFinishPosition = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z + startPosition);
        //        }
        //        else
        //        {
        //            newFinishPosition = new Vector3(finishPosition.x, finishPosition.y, finishPosition.z - startPosition);
        //        }
        //    }

        //    GameObject newWall = Instantiate(prefabWall, newFinishPosition, Quaternion.identity);

        //    newWall.transform.localScale = new Vector3(newWall.transform.localScale.x, targetHeightScale, newWall.transform.localScale.z);

        //    startPosition += startPositionConst;
        //    Debug.Log(startPosition);


        //}

        ////GameObject newWall = Instantiate(prefabWall, finishPosition, Quaternion.identity);

        ////newWall.transform.localScale = new Vector3(1, targetHeightScale, 1);
        ////newWall.transform.position = new Vector3(newWall.transform.position.x, newWall.transform.localScale.y / 2f, newWall.transform.position.z);
    }

    private void CreateWall(float targetX, float targetZ)
    {
        Vector2 targetPosition = new Vector2(startCoordinates.x + targetX, startCoordinates.y + targetZ);
        Vector3 finishPosition = new Vector3(targetPosition.x, 0.5f, targetPosition.y);

        GameObject newWall = Instantiate(prefabWall, finishPosition, Quaternion.identity);

        if (isHighWalls)
        {
            newWall.transform.localScale = new Vector3(newWall.transform.localScale.x, targetHeightScale, newWall.transform.localScale.z);

            finishPosition = new Vector3(newWall.transform.position.x, targetHeightScale / 2f, newWall.transform.position.z);
            newWall.transform.position = finishPosition;
        }

        newWall.transform.localScale = new Vector3(1, 1, 1);
        newWall.transform.parent = mainPlane.transform;

    }

    private void CreatePlayer(float targetX, float targetZ)
    {
        ////////////Vector2 targetPosition = new Vector2(startCoordinates.x + targetX, startCoordinates.y + targetZ);
        ////////////Vector3 finishPosition = new Vector3(targetPosition.x, 0.5f, targetPosition.y);
        ////////////GameObject newPlayer = Instantiate(prefabPlayer, finishPosition, Quaternion.identity);
    }
}
