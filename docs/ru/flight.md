# Семейство скриптов компонента Flight

## [Flight](./../../Runtime/Code/Flight/Flight.cs)

Управляет полетом объекта.

### Какие действия выполняет

Применяет силу к `Rigidbody` для получения заданных характеристик движения и вращения объекта.

### Свойства компонента

- Vector3 targetPoint - точка в пространстве, в направлении которой будет разворачиваться объект
- float throttle - тяга двигателя
- float maxSpeed - максимальная скорость полета при тяге 100% (м/с)
- Vector3 maxTorque - максимальная угловая скорость (rad/s)
- float sensivityAngle - чувствительность управления в рамках угла атаки
- float engineSensivity - чувствительность на изменение throttle
- float steeringSensivity - чувствительность на изменение направления движения к targetPoint
- float movementDumping - параметр влияния энертности движения
- float torqueDumping - параметр влияния энертности вращения
- float autoHorizontFactor - модификатор силы выравнивания объекта в горизонтальное положение

**Реагирует на следующие события:**

```c#
// Установка тяги
OnThrottleSet(float throttle)

// Установка точки движения, в направлении которой неободимо выровнять объект
OnTargetPointSet(Vector3 targetPoint)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [AutoThrottle](./../../Runtime/Code/Flight/AutoThrottle.cs)

Автоматизация работы с тягой двигателя.

### Какие действия выполняет

Обрабатывает события входа в зону досягаемости и выравниания на цель. При соблюдении обоих условий, активирует переключает тягу двигателя между указанными значениями.

> Данный компонент наследуется от [AbstractReaction](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractreaction)

### Свойства компонента

- float lowThrottle - используется при не соответствии заданным условиям
- float highThrottle - используется при соответствии заданным условиям

**Реагирует на следующие события:**

```c#
// Выравнивание на цель
OnAlignment(bool inAlignment)

// Вход в зону досягаемости
OnRange(bool inRange)
```

**Создает следующие сквозные события:**

```c#
// Установка тяги Flight
OnThrottleSet(float throttle)
```

## [FlightEngineAudioController](./../../Runtime/Code/Flight/FlightEngineAudioController.cs)

Управляет воспроизведением звука двигателя [Flight](./../../Runtime/Code/Flight/Flight.cs).

### Какие действия выполняет

Отслеживает текущее значение мощности двигателя [Flight](./../../Runtime/Code/Flight/Flight.cs) путем расчета его текущей мощности по отношению к максимальной, и меняет значения громкости, а также тона воспроизводимого звука в соответствии с вычислениями.

> Данный компонент наследуется от [AbstractAudioController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractaudiocontroller)

### Свойства компонента

- GameObject target - целевой объект, содержащий компонент [Flight](./../../Runtime/Code/Flight/Flight.cs)
- AnimationCurve volume - кривая громкости звука по отношению к текущей мощности двигателя, где  1 - мощность двигателя при полной тяге
- AnimationCurve pitch - кривая тона звука по отношению к текущей мощности двигателя, где  1 - мощность двигателя при полной тяге

**Реагирует на следующие события:**

```c#
// Сбрасывает целевой объект
OnControlTargetBreak()

// Устанавливает целевой объект
OnControlTargetSet(GameObject target)

// Устанавливает набор воспроизводимых звуков из внешней системы управления ресурсами и запускает воспроизведение
OnAssetLoaded(Object[] audioClips)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [FlyByAudioController](./../../Runtime/Code/Flight/FlyByAudioController.cs)

Воспроизводит звук при прохождени объекта мимо триггера.

### Какие действия выполняет

Обрабатывает события, получаемые от `Collider` с `IsTrigger = true`, и воспроизводит звук, в случае его пересечения в другим `Collider`. Подробнее о работе `Collider` в можно прочесть в [документации Unity](https://docs.unity3d.com/ScriptReference/Collider.html)

> Данный компонент наследуется от [AbstractAudioController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractaudiocontroller)

### Свойства компонента

- отсутствуют

**Реагирует на следующие события:**

```c#
// Вход триггера в другой коллайдер или триггер
OnTriggerEnter(Collider other)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [RangeSensivityPointSwitcher](./../../Runtime/Code/Flight/RangeSensivityPointSwitcher.cs)

Переключает sensivityPoint компонента [Flight](./../../Runtime/Code/Flight/Flight.cs).

### Какие действия выполняет

Обрабатывает события входа в зону досягаемости и переключает sensivityPoint компонента [Flight](./../../Runtime/Code/Flight/Flight.cs), между начальным значением и `inRangeSensivityAngle`.

### Свойства компонента

- float inRangeSensivityAngle - чувствительность управления используемая в зоне досягаемости

**Реагирует на следующие события:**

```c#
// Вход в зону досягаемости
OnRange(bool inRange)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [CondensationTrailFXController](./../../Runtime/Code/FXControllers/CondensationTrailFXController.cs)

Управляет активацией эффекта конденсационного следа

### Какие действия выполняет

Отслеживает текущую высоту и если она превышает указанный порог - активирует эффект. Деактивация эффекта происходит при прохождении указанного порога в обратном направлении.

### Свойства компонента

- float altitudeThreshold - порог высоты активации

**Реагирует на следующие события:**

```c#
// Настройка эффекта после его спавна внешней системой управления ресурсами
OnAssetInstantiated(GameObject asset)
```

## [EngineFlameFXController](./../../Runtime/Code/FXControllers/EngineFlameFXController.cs)

Управляет эффектом пламени двигателя.

### Какие действия выполняет

Отслеживает текущее значение мощности двигателя [Flight](./../../Runtime/Code/Flight/Flight.cs) путем расчета его текущей мощности по отношению к максимальной, и меняет масштаб эффекта и его прозрачность, в соответствии с указанным параметром интенсивности.

### Свойства компонента

- AnimationCurve intensity - интенсивность изменения масштаба и прозрачности эффекта по отношению к текущей мощности двигателя, где 1 - мощность двигателя при полной тяге

**Реагирует на следующие события:**

```c#
// Настройка эффекта после его спавна внешней системой управления ресурсами
OnAssetInstantiated(GameObject asset)
```

## [FlowBreakFXController](./../../Runtime/Code/FXControllers/FlowBreakFXController.cs)

Управляет эффектом срыва потока.

### Какие действия выполняет

Отслеживает вертикальную скорость `Rigidbody` и если она превышает указанный порог - активирует эффект. Деактивация эффекта происходит при прохождении указанного порога в обратном направлении.

### Свойства компонента

- AnimationCurve intensity - интенсивность эффекта по отношеню к вертикальной скорости объекта

**Реагирует на следующие события:**

```c#
// Настройка эффекта после его спавна внешней системой управления ресурсами
OnAssetInstantiated(GameObject asset)
```

## [SonicBarrierFXController](./../../Runtime/Code/FXControllers/SonicBarrierFXController.cs)

Управляет эффектом прохода звукового барьера.

### Какие действия выполняет

Отслеживает скорость в направлении продольной оси объекта и если она превышает указанный порог - активирует эффект. Деактивация эффекта происходит при прохождении указанного порога в обратном направлении.

### Свойства компонента

- AnimationCurve intensity - интенсивность эффекта по отношеню к скорости объекта
- AnimationCurve shift - смещения эффекта в продольной оси по отношению к скорости объекта, value должно меняться в диапазоне от 0 - 1

**Реагирует на следующие события:**

```c#
// Настройка эффекта после его спавна внешней системой управления ресурсами
OnAssetInstantiated(GameObject asset)
```

## [WingMechanizationFXController](./../../Runtime/Code/FXControllers/WingMechanizationFXController.cs)

Управляет анимацией механизации крыла

### Какие действия выполняет

Отслеживает текущее значение силы вращения [Flight](./../../Runtime/Code/Flight/Flight.cs) путем расчета его текущих значений по отношению к максимальным. Вращает указанные объекты в соответствии с вычислениями. Расположение элементов механизации в отношении центра объекта инвертирует значения поворта в зависимости от типа механизации.

### Свойства компонента

- Transform[] mechanization - набор элементов механизации
- string pathToMechanization - расположение элемента механизации в загруженном объекте 3д модели для получения доступа к объектам механизации (работает только при загрузке основной 3д модели внешней системой управления ресурсами)
- MechanizationType main - основной тип механизации (pitch, yaw, roll)
- MechanizationType mix - дополнительны тип механизации, используется для смешивания механизаций, например на современных реактивных истребяителях руль высоты (хвостовое оперение) дублирует поведение элеронов и учавствует в крене самолета
- Vector3 rotationAxis - ось вращения элемента механизации
- float maxAttackAngle - максимальный угол отклонения элемента механизации

**Реагирует на следующие события:**

```c#
// Настройка объектов механизации при загрузке 3д модели внешней системо управления ресурсами
OnAssetInstantiated(GameObject asset)
```

## Набор компонентов для отображения характеристик в текстовом виде

> Данная группа компонентов наследуется от [AbstractTextMeshProDisplayController и AbstractTextMeshProNumericalDisplayController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracttextmeshprodisplaycontroller-и-abstracttextmeshpronumericaldisplaycontroller)

[FlightThrottleDisplayController](./../../Runtime/Code/Flight/FlightThrottleDisplayController.cs)

[FlightMaxPitchTorqueDisplayController](./../../Runtime/Code/Flight/FlightMaxPitchTorqueDisplayController.cs)

[FlightMaxRollTorqueDisplayController](./../../Runtime/Code/Flight/FlightMaxRollTorqueDisplayController.cs)

[FlightMaxYawTorqueDisplayController](./../../Runtime/Code/Flight/FlightMaxYawTorqueDisplayController.cs)

[FlightMaxSpeedDisplayController](./../../Runtime/Code/Flight/FlightMaxSpeedDisplayController.cs)
